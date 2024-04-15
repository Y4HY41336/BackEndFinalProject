using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
