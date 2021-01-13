using Microsoft.AspNetCore.Mvc;

namespace TestTaskForSimbirSoft.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            return View("Index");
        }
    }
}
