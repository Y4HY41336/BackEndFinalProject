using FinalProject.Context;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Where(p => p.isStocked).Where(p => !p.isDeleted).Include(p => p.Category).ToListAsync();
            var categories = await _context.Categories.Where(p => !p.isDeleted).ToListAsync();
            HomePageViewModel model = new()
            {
                Products = products,
                Categories = categories

            };
            return View(model);
        }
    }
}
