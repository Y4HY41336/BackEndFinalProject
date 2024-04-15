using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Areas.Admin.Controllers;
[Area("Admin")]

public class CategoryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
