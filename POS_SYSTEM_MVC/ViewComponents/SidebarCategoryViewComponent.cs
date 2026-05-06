using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.Services.CategoryServices;
using System.Threading.Tasks;

namespace POS_SYSTEM_MVC.ViewComponents
{
    public class SidebarCategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public SidebarCategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            return View(categories);
        }
    }
}
