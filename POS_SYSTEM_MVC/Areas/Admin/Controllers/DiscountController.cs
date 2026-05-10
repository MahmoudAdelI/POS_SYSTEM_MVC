using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POS_SYSTEM_MVC.Constants;
using POS_SYSTEM_MVC.Repositories.DiscountRepo;
using POS_SYSTEM_MVC.Repositories.ProductRepo;
using POS_SYSTEM_MVC.Repositories.ProductVariantRepo;
using POS_SYSTEM_MVC.Services.DiscountServices;
using POS_SYSTEM_MVC.ViewModels;

namespace POS_SYSTEM_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Admin)]
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountRepository discountRepository,IProductRepository productRepository,IProductVariantRepository productVariantRepository, IDiscountService discountService)
        {
            _discountRepository = discountRepository;
            _productRepository = productRepository;
            _productVariantRepository = productVariantRepository;
            _discountService = discountService;
        }
        public async Task< IActionResult> Index()
        {
            var discounts =
                   await _discountRepository.GetAllAsync();

            var products =
                await _productRepository.GetAllAsync();

            var variants =
                await _productVariantRepository.GetAllAsync();

            var vm = new DiscountformVM
            {
                Discounts = discounts,
                Products = products,

                ProductVariants= variants

            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult>Index(DiscountformVM vm)
        {
            var errors =await _discountService.CreateAsync(vm.Discount);

            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(
                        "",
                        error);
                }

                vm.Discounts =await _discountService.GetAllAsync();

                vm.Products =await _productRepository.GetAllAsync();

                vm.ProductVariants =await _productVariantRepository .GetAllAsync();

                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
