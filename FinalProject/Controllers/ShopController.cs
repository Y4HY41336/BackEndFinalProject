using FinalProject.Context;
using FinalProject.Migrations;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


    }
}
