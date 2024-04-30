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
            var productsCookieViewModel = new List<BasketItem>();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var basketItems = await _context.BasketItems.Where(b => b.AppUserId == user.Id).ToListAsync();

                return View(basketItems);
            }
            return View(productsCookieViewModel);
        }
    }
}
