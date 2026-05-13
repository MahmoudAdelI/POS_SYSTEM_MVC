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

        public async Task<IActionResult> Index()
        {
            var discounts = await _unitOfWork.Discounts.GetAllAsync();
            var products = await _unitOfWork.Products.GetAllAsync();
            var variants = await _unitOfWork.ProductVariants.GetAllAsync();

            var vm = new DiscountformVM
            {
                Discounts = discounts,
                Products = products,
                ProductVariants = variants
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(DiscountformVM vm)
        {
            var errors = await _discountService.CreateAsync(vm.Discount);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }

                vm.Discounts = await _discountService.GetAllAsync();
                vm.Products = await _unitOfWork.Products.GetAllAsync();
                vm.ProductVariants = await _unitOfWork.ProductVariants.GetAllAsync();

                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
