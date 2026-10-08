using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cacatone_2026.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
