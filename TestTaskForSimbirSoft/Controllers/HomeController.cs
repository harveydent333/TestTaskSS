using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Text;
using System.IO;
using TestTaskForSimbirSoft.Infrastructure;
using TestTaskForSimbirSoft.Models;

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
