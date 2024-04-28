using FinalProject.Context;
using FinalProject.Migrations;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var products = await _context.Products/*.Where(p => p.isStocked)*/.Where(p => !p.isDeleted).Include(p => p.Category).ToListAsync();
            return View(products);

        }
        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            if (model != null)
            {
                var searchTerm = model.SearchTerm.ToLower();
                var filteredProducts = await _context.Products.Where(p => p.Title.ToLower().Contains(searchTerm)).Where(p => !p.isDeleted).Include(p => p.Category).ToListAsync();
                return View(filteredProducts);
            }
            else
            {
                return View(null);
            }
        }


    }
}
