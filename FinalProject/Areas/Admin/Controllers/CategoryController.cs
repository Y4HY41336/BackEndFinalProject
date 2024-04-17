using FinalProject.Areas.Admin.ViewModel;
using FinalProject.Context;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Areas.Admin.Controllers;
[Area("Admin")]

public class CategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public CategoryController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var category = _context.Categories.ToList();

        return View(category);
    }
    public async Task<IActionResult> Create()
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CategoryViewModel category)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (category.Image.CheckFileSize(3000))
        {
            ModelState.AddModelError("Image", "Too Big!");
            return View();
        }
        if (!category.Image.CheckFileType("image/"))
        {
            ModelState.AddModelError("Image", "sekil olsun");
            return View();
        }
        string fileName = $"{Guid.NewGuid()}-{category.Image.FileName}";
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "categories", fileName);
        FileStream stream = new FileStream(path, FileMode.Create);
        await category.Image.CopyToAsync(stream);
        stream.Dispose();
        Category newcategory = new()
        {
            CategoryName = category.CategoryName,
            Image = fileName
        };
        await _context.Categories.AddAsync(newcategory);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        _context.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Detail(int id)
    {

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    public async Task<IActionResult> Update(int id)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, CategoryViewModel category)
    {
        if (category == null)
        {
            return NotFound();
        }
        var updatecategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        if (!ModelState.IsValid)
        {
            return View();
        }

        updatecategory.CategoryName = category.CategoryName;

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
