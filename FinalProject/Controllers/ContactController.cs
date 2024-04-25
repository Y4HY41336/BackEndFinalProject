using FinalProject.Context;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
	public class ContactController : Controller
	{
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(Comment comment)
		{
            if (!ModelState.IsValid)
                return View();

            Comment comments = new()
			{
				Name = comment.Name,
				Comments = comment.Comments,
				Email = comment.Email,
			};
            await _context.Comments.AddAsync(comments);
            await _context.SaveChangesAsync();

            if (ModelState.IsValid)
            {
                TempData["Success"] = "Your comment sent successfully";
                return RedirectToAction("Index");
            }
            return View();

        }
	}
}
