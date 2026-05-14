using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.Constants;
using POS_SYSTEM_MVC.Services.DiscountServices;
using POS_SYSTEM_MVC.UnitOfWork;
using POS_SYSTEM_MVC.ViewModels;

namespace POS_SYSTEM_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Admin)]
    public class DiscountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDiscountService _discountService;

        public DiscountController(IUnitOfWork unitOfWork, IDiscountService discountService)
        {
            _unitOfWork = unitOfWork;
            _discountService = discountService;
        }

        public async Task<IActionResult> Index(string searchTerm = null, POS_SYSTEM_MVC.Constants.Enums.DiscountTypeENUM? filterType = null, bool? filterIsActive = null, int page = 1)
        {
            int pageSize = 10;
            var paginatedResult = await _unitOfWork.Discounts.GetPaginatedAsync(searchTerm, page, pageSize, filterType, filterIsActive);
            var products = await _unitOfWork.Products.GetAllAsync();
            var variants = await _unitOfWork.ProductVariants.GetAllAsync();

            int totalPages = (int)Math.Ceiling(paginatedResult.TotalCount / (double)pageSize);

            var vm = new DiscountformVM
            {
                Discounts = paginatedResult.Discounts.ToList(),
                Products = products,
                ProductVariants = variants,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                FilterType = filterType,
                FilterIsActive = filterIsActive
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DiscountformVM vm, string searchTerm = null, POS_SYSTEM_MVC.Constants.Enums.DiscountTypeENUM? filterType = null, bool? filterIsActive = null, int page = 1)
        {
            var errors = await _discountService.CreateAsync(vm.Discount);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }

                int pageSize = 10;
                var paginatedResult = await _unitOfWork.Discounts.GetPaginatedAsync(searchTerm, page, pageSize, filterType, filterIsActive);
                int totalPages = (int)Math.Ceiling(paginatedResult.TotalCount / (double)pageSize);

                vm.Discounts = paginatedResult.Discounts.ToList();
                vm.Products = await _unitOfWork.Products.GetAllAsync();
                vm.ProductVariants = await _unitOfWork.ProductVariants.GetAllAsync();
                vm.CurrentPage = page;
                vm.TotalPages = totalPages;
                vm.PageSize = pageSize;
                vm.SearchTerm = searchTerm;
                vm.FilterType = filterType;
                vm.FilterIsActive = filterIsActive;

                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
