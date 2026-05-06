using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using System.Threading.Tasks;

namespace POS_SYSTEM_MVC.ViewComponents
{
    public class SidebarCategoryViewComponent : ViewComponent
    {
        private readonly POSContext _context;

        public SidebarCategoryViewComponent(POSContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                                           .Include(c => c.SubCategories)
                                           .ToListAsync();

            return View(categories);
        }
    }
}
