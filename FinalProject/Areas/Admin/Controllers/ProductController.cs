using FinalProject.Context;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace FinalProject.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var products = _context.Products.ToList();


        return View(products);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product products)
    {
        //Product newproduct = new()
        //{
        //    Title = products.Title,
        //    Price = products.Price,
        //    OldPrice = products.OldPrice,
        //    Rating = products.Rating,
        //    SKU = products.SKU,
        //    Description = products.Description,
        //    Features = products.Features,
        //    Material = products.Material,
        //    Manufacturer = products.Manufacturer,
        //    ClaimedSize = products.ClaimedSize,
        //    RecommendedUse = products.RecommendedUse,
        //    isStocked = true
        //};
        await _context.Products.AddAsync(products);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    //public async Task<IActionResult> Delete(int id)
    //{
    //    var slider = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
    //    if (slider == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(slider);
    //}

    //[HttpPost]
    //[ActionName("Delete")]
    //public async Task<IActionResult> DeleteSlider(int id)
    //{
    //    var slider = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
    //    if (slider == null)
    //    {
    //        return NotFound();
    //    }
    //    string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", slider.Image);
    //    if (System.IO.File.Exists(path))
    //    {
    //        System.IO.File.Delete(path);
    //    }
    //    _context.Remove(slider);
    //    await _context.SaveChangesAsync();
    //    return RedirectToAction("Index");
    //}
}
