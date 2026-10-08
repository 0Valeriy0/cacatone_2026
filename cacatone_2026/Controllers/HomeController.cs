using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cacatone_2026.Controllers
{
    [Route("")]
    [Route("Home")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
