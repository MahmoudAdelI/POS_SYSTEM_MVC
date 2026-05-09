using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.Constants;
using POS_SYSTEM_MVC.Services.SalesHistoryServices;

namespace POS_SYSTEM_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Role.Admin)]
    public class SalesHistoryController : Controller
    {
        private readonly ISalesHistoryService _salesHistoryService;

        public SalesHistoryController(ISalesHistoryService salesHistoryService)
        {
            _salesHistoryService = salesHistoryService;
        }

        public async Task<IActionResult> Index(string? filter = "all")
        {
            ViewData["Filter"] = filter;
            var data = await _salesHistoryService.GetSalesHistoryAsync(filter);
            return View(data);
        }
    }
}