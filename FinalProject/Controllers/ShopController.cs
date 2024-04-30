using FinalProject.Context;
using FinalProject.Migrations;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinalProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ShopController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public async Task<IActionResult> LoadMore(int skip)
        {
            var products = await _context.Products.Where(p => !p.isDeleted).Skip(skip).Take(3).Include(p => p.Category).ToListAsync();
            //var productImages = await _context.ProductImages.ToListAsync();

            //var productImageViewModel = new ProductImageViewModel
            //{
            //    Products = products,
            //    ProductImages = productImages
            //};

            return PartialView("_ProductPartial", products);
        }
        [Authorize]
        public async Task<IActionResult> AddToBasket(int productId)
        {
            var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (products == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.ProductId == productId && b.AppUserId == user.Id);

            if (basketItem == null)
            {
                BasketItem newBasketItem = new()
                {
                    ProductId = productId,
                    AppUserId = user.Id,
                    Count = 1,

                };

                await _context.BasketItems.AddAsync(newBasketItem);
            }
            else
            {
                basketItem.Count++;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> DeleteBasket(int productId)
        {
            var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (products == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.ProductId == productId && b.AppUserId == user.Id);

            if (basketItem.Count <= 1)
            {
                _context.Remove(basketItem);

            }
            else if(basketItem.Count > 1)
            {
                basketItem.Count--;
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");


        }

    }
}
