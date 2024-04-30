using FinalProject.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {

        private readonly AppDbContext _context;

        public BlogViewComponent(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blog = await _context.Blogs.Where(b => !b.isDeleted).Take(3).ToListAsync();
            return View(blog);
        }

    }
}
