using FinalProject.Context;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.ViewComponents

{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
