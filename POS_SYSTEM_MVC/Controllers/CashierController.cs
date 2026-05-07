using Microsoft.AspNetCore.Mvc;
using POS_SYSTEM_MVC.Services.ProductServices;
using System.Linq;

namespace POS_SYSTEM_MVC.Controllers
{
    public class CashierController : Controller
    {
        private readonly IProductService _productService;

        public CashierController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string searchTerm, int page = 1)
        {
            const int pageSize = 8;
            var result = await _productService.GetProductsForCashierAsync(searchTerm, page, pageSize);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)result.TotalItems / pageSize);

            return View(result.Products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var product = await _productService.GetProductDetailsAsync(id);

            if (product == null) return NotFound();

            return PartialView("_ProductDetailsModal", product);
        }
    }
}