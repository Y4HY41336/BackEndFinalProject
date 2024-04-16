using FinalProject.Context;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        if (!ModelState.IsValid)
        {
            return View();
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel products)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Product newproduct = new()
        {
            Title = products.Title,
            Price = products.Price,
            OldPrice = products.OldPrice,
            Rating = products.Rating,
            SKU = products.SKU,
            Description = products.Description,
            Features = products.Features,
            Material = products.Material,
            Manufacturer = products.Manufacturer,
            ClaimedSize = products.ClaimedSize,
            RecommendedUse = products.RecommendedUse,
            CategoryId = 1,
            BrandId = 2,
            isStocked = true,
            isDeleted = false

        };
        await _context.Products.AddAsync(newproduct);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        
        var products = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if (products == null)
        {
            return NotFound();
        }
        return View(products);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        
        var products = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if (products == null)
        {
            return NotFound();
        }
        //string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", products.Image);
        //if (System.IO.File.Exists(path))
        //{
        //    System.IO.File.Delete(path);
        //}
        _context.Remove(products);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Detail(int id)
    {

        var products = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if (products == null)
        {
            return NotFound();
        }
        return View(products);
    }
}
