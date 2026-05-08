using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public CashierController(IProductService productService, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchTerm, int? categoryId, int? subCategoryId, string stockFilter = "all", int page = 1)
        {
            const int pageSize = 12; // Increased pageSize for better layout
            var result = await _productService.GetProductsForCashierAsync(searchTerm, categoryId, subCategoryId, stockFilter, page, pageSize);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryId = subCategoryId;
            ViewBag.StockFilter = stockFilter;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)result.TotalItems / pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
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
                var receiptItems = new List<object>();

                foreach (var item in request.Items)
                {
                    var variant = await _unitOfWork.Products.GetVariantByIdAsync(item.ProductVariantId);
                    if (variant == null) continue;

                    var saleLine = new SaleLine
                    {
                        ProductVariantId = item.ProductVariantId,
                        Quantity = item.Quantity,
                        OriginalUnitPrice = variant.UnitPrice,
                    };

                    sale.SaleLines.Add(saleLine);
                    subtotal += variant.UnitPrice * item.Quantity;

                    var variantInfo = string.Join(", ", variant.VariantAttributes.Select(va => va.AttributeValue.Value));
                    receiptItems.Add(new
                    {
                        name = variant.Product.Name,
                        variantInfo = variantInfo,
                        quantity = item.Quantity,
                        unitPrice = (double)variant.UnitPrice
                    });

                    // Update stock
                    variant.StockQuantity -= item.Quantity;
                }

                await _unitOfWork.Sales.AddAsync(sale);
                await _unitOfWork.SaveChangesAsync();

                var tax = subtotal * 0.10m;
                var total = subtotal + tax;

                return Json(new
                {
                    success = true,
                    receiptData = new
                    {
                        saleId = sale.Id,
                        date = sale.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                        items = receiptItems,
                        subtotal = (double)subtotal,
                        tax = (double)tax,
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
    }
}
