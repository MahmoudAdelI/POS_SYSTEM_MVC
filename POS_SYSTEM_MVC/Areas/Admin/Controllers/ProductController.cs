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
            var vm = new CreateProductViewModel
            {
                Categories = await categoryService.GetAllWithSubsAsync(),
                Units = await unitService.GetAllAsync(),
                Brands = await brandService.GetAllAsync()
            };

            return View(vm);
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] AddProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await productService.AddProductWithVariants(productDto);
                return Ok("Product created successfully");
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { message = "Failed to create product", detail = ex.Message });
            }
        }
    }
}
