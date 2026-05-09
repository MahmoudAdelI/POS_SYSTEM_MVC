using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.Constants;
using POS_SYSTEM_MVC.DTOs.Product;
using POS_SYSTEM_MVC.Services.Brands;
using POS_SYSTEM_MVC.Services.CategoryServices;
using POS_SYSTEM_MVC.Services.ProductServices;
using POS_SYSTEM_MVC.Services.UnitServices;
using POS_SYSTEM_MVC.ViewModels;

namespace POS_SYSTEM_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Admin)]
    [Route("/products")]
    public class ProductController(ICategoryService categoryService,
        IUnitService unitService,
        IBrandService brandService,
        IProductService productService
        ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllWithSubsAsync();
            var units = await unitService.GetAllAsync();
            var brands = await brandService.GetAllAsync();
            var vm = new CreateProductViewModel { Categories = categories, Units = units, Brands = brands };
            return View(vm);
        }

        [HttpPost("/products/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] AddProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await productService.AddProductWithVariants(productDto);

            return Ok(productDto);
        }
    }
}
