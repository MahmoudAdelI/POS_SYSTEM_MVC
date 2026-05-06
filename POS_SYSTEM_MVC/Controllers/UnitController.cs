using Microsoft.AspNetCore.Mvc;

namespace POS_SYSTEM_MVC.Controllers
{
    public class UnitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
