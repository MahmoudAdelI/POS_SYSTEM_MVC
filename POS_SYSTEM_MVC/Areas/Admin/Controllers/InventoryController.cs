using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.Constants;
using POS_SYSTEM_MVC.Services.InventoryServices;

namespace POS_SYSTEM_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]          
    [Authorize(Roles = Role.Admin)]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            ViewData["Search"] = search;
            var data = await _inventoryService.GetInventoryDataAsync(search);
            return View(data);
        }
    }
}