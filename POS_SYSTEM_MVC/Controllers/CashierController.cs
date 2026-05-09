using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.DTOs.Checkout;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Services.ProductServices;
using POS_SYSTEM_MVC.UnitOfWork;
using System.Linq;
using System.Security.Claims;

namespace POS_SYSTEM_MVC.Controllers
{
    public class CashierController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly POSContext _context;

        public CashierController(IProductService productService, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, POSContext context)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTerm, int? categoryId, int? subCategoryId, string stockFilter = "all", int page = 1, int pageSize = 12)
        {
            var result = await _productService.GetProductsForCashierAsync(searchTerm, categoryId, subCategoryId, stockFilter, page, pageSize);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            ViewBag.StockFilter = stockFilter;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = result.TotalItems;
            ViewBag.TotalPages = (int)Math.Ceiling((double)result.TotalItems / pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" && Request.Query.ContainsKey("partial"))
            {
                return PartialView("_ProductList", result.Products);
            }

            return View(result.Products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var product = await _productService.GetProductDetailsAsync(id);

            if (product == null) return NotFound();

            return PartialView("_ProductDetailsModal", product);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveDiscountRules()
        {
            var now = DateTime.Now;

            var discounts = await _context.Discounts
                .AsNoTracking()
                .Where(d => d.IsActive && (!d.ExpiresAt.HasValue || d.ExpiresAt > now))
                .Select(d => new
                {
                    id = d.Id,
                    type = d.Type.ToString(),
                    value = (double)d.Value,
                    productId = d.ProductId,
                    productVariantId = d.ProductVariantId,
                    saleTotalThreshold = d.SaleTotalThreshold.HasValue ? (double?)d.SaleTotalThreshold.Value : null,
                    createdAt = d.CreatedAt
                })
                .ToListAsync();

            return Json(new { success = true, discounts });
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto request)
        {
            if (request == null || !request.Items.Any())
            {
                return Json(new { success = false, message = "Cart is empty." });
            }

            try
            {
                // Get the current logged-in user ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // For testing/development: if not logged in, try to find a valid user ID to avoid Foreign Key errors
                if (string.IsNullOrEmpty(userId))
                {
                    // Attempt 1: Find the seeded 'cashier' user
                    var cashierUser = await _userManager.FindByNameAsync("cashier");
                    userId = cashierUser?.Id;

                    if (string.IsNullOrEmpty(userId))
                    {
                        // Attempt 2: Just find ANY user ID in the system
                        var anyUser = await _userManager.Users.FirstOrDefaultAsync();
                        userId = anyUser?.Id;
                    }

                    if (string.IsNullOrEmpty(userId))
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Checkout failed: No users found in the system. Please ensure database seeding has run correctly."
                        });
                    }
                }

                var sale = new Sale
                {
                    CashierId = userId,
                    CreatedAt = DateTime.Now,
                    Status = SaleStatus.Completed,
                    SaleLines = new List<SaleLine>()
                };

                decimal subtotal = 0;
                decimal lineDiscountTotal = 0;
                var receiptItems = new List<object>();
                var activeDiscounts = await GetActiveDiscountsAsync();

                foreach (var item in request.Items)
                {
                    if (item.Quantity <= 0) continue;

                    var variant = await _unitOfWork.Products.GetVariantByIdAsync(item.ProductVariantId);
                    if (variant == null) continue;

                    var lineSubtotal = variant.UnitPrice * item.Quantity;
                    var lineDiscountRule = GetLineDiscountRule(activeDiscounts, variant.Id, variant.ProductId);
                    var lineDiscountAmount = CalculateDiscountAmount(lineDiscountRule, lineSubtotal);
                    var lineTotal = lineSubtotal - lineDiscountAmount;

                    var saleLine = new SaleLine
                    {
                        ProductVariantId = item.ProductVariantId,
                        Quantity = item.Quantity,
                        OriginalUnitPrice = variant.UnitPrice,
                        DiscountType = lineDiscountRule?.Type,
                        DiscountValue = lineDiscountRule?.Value,
                        DiscountAmount = lineDiscountAmount > 0 ? lineDiscountAmount : null,
                    };

                    sale.SaleLines.Add(saleLine);
                    subtotal += lineSubtotal;
                    lineDiscountTotal += lineDiscountAmount;

                    var variantInfo = string.Join(", ", variant.VariantAttributes.Select(va => va.AttributeValue.Value));
                    receiptItems.Add(new
                    {
                        name = variant.Product.Name,
                        variantInfo = variantInfo,
                        quantity = item.Quantity,
                        unitPrice = (double)variant.UnitPrice,
                        discountAmount = (double)lineDiscountAmount,
                        lineTotal = (double)lineTotal
                    });

                    // Update stock
                    variant.StockQuantity -= item.Quantity;
                }

                if (!sale.SaleLines.Any())
                {
                    return Json(new { success = false, message = "Cart has no valid items." });
                }

                var subtotalAfterLineDiscount = subtotal - lineDiscountTotal;
                var orderDiscountRule = GetOrderDiscountRule(activeDiscounts, subtotalAfterLineDiscount);
                var orderDiscountAmount = CalculateDiscountAmount(orderDiscountRule, subtotalAfterLineDiscount);

                sale.DiscountType = orderDiscountRule?.Type;
                sale.DiscountValue = orderDiscountRule?.Value;
                sale.DiscountAmount = orderDiscountAmount > 0 ? orderDiscountAmount : null;

                var totalDiscount = lineDiscountTotal + orderDiscountAmount;
                var total = subtotal - totalDiscount;

                await _unitOfWork.Sales.AddAsync(sale);
                await _unitOfWork.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    receiptData = new
                    {
                        saleId = sale.Id,
                        date = sale.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                        items = receiptItems,
                        subtotal = (double)subtotal,
                        lineDiscountTotal = (double)lineDiscountTotal,
                        orderDiscount = (double)orderDiscountAmount,
                        totalDiscount = (double)totalDiscount,
                        total = (double)total
                    }
                });
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                if (ex.InnerException != null) errorMsg += " | Inner: " + ex.InnerException.Message;
                return Json(new { success = false, message = "Checkout failed: " + errorMsg });
            }
        }

        private async Task<List<Discount>> GetActiveDiscountsAsync()
        {
            var now = DateTime.Now;
            return await _context.Discounts
                .AsNoTracking()
                .Where(d => d.IsActive && (!d.ExpiresAt.HasValue || d.ExpiresAt > now))
                .ToListAsync();
        }

        private static Discount? GetLineDiscountRule(IEnumerable<Discount> discounts, int variantId, int productId)
        {
            var variantRule = discounts
                .Where(d => !d.SaleTotalThreshold.HasValue && d.ProductVariantId == variantId)
                .OrderByDescending(d => d.CreatedAt)
                .FirstOrDefault();

            if (variantRule != null)
            {
                return variantRule;
            }

            return discounts
                .Where(d => !d.SaleTotalThreshold.HasValue && !d.ProductVariantId.HasValue && d.ProductId == productId)
                .OrderByDescending(d => d.CreatedAt)
                .FirstOrDefault();
        }

        private static Discount? GetOrderDiscountRule(IEnumerable<Discount> discounts, decimal subtotalAfterLineDiscount)
        {
            return discounts
                .Where(d => d.SaleTotalThreshold.HasValue && subtotalAfterLineDiscount >= d.SaleTotalThreshold.Value)
                .OrderByDescending(d => d.CreatedAt)
                .FirstOrDefault();
        }

        private static decimal CalculateDiscountAmount(Discount? rule, decimal amountBase)
        {
            if (rule == null || amountBase <= 0)
            {
                return 0;
            }

            decimal discountAmount = rule.Type switch
            {
                DiscountType.Fixed => rule.Value,
                DiscountType.Percentage => amountBase * (rule.Value / 100m),
                _ => 0
            };

            if (discountAmount < 0)
            {
                discountAmount = 0;
            }

            if (discountAmount > amountBase)
            {
                discountAmount = amountBase;
            }

            return Math.Round(discountAmount, 2);
        }
    }
}
