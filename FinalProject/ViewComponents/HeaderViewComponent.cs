using FinalProject.Context;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HeaderViewComponent(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderViewModel model = new();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var basketItems = await _context.BasketItems.Where(b => b.AppUserId == user.Id).Include(b => b.Product).ToListAsync();
                model.BasketItem = basketItems;
                model.TotalPrice = Math.Round(basketItems.Sum(b => b.Product.Price * b.Count), 2);
            }



            return View(model);

        }
    }
}
