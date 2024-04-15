using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
