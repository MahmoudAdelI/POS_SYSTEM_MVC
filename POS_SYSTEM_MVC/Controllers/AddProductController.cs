using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.Services.Brands;
using POS_SYSTEM_MVC.Services.CategoryServices;
using POS_SYSTEM_MVC.Services.UnitServices;
using POS_SYSTEM_MVC.ViewModels;

namespace POS_SYSTEM_MVC.Controllers
{
    public class AddProductController(ICategoryService categoryService, IUnitService unitService, IBrandService brandService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllWithSubsAsync();
            var units = await unitService.GetAllAsync();
            var brands = await brandService.GetAllAsync();
            var vm = new CreateProductViewModel { Categories = categories, Units = units, Brands = brands };
            return View(vm);
        }
    }
}
