using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
