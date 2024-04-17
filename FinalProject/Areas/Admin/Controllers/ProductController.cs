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
public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        if (!ModelState.IsValid)
        {
            return View();
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel products)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        //if (!modelstate.isvalid)
        //{
        //    return view();
        //}
        //if (products.image.checkfilesize(3000))
        //{
        //    modelstate.addmodelerror("image", "too big!");
        //    return view();
        //}
        //if (!products.image.checkfiletype("image/"))
        //{
        //    modelstate.addmodelerror("image", "sekil olsun");
        //    return view();
        //}
        //string filename = $"{guid.newguid()}-{products.image.filename}";
        //string path = path.combine(_webhostenvironment.webrootpath, "assets", "images", "categories", filename);
        //filestream stream = new filestream(path, filemode.create);
        //await products.image.copytoasync(stream);
        //stream.dispose();
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
            CategoryId = products.CategoryId,
            BrandId = products.BrandId,
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
    public async Task<IActionResult> Update(int id)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        var products = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if (products == null)
        {
            return NotFound();
        }
        return View(products);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, ProductViewModel products)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        var updateProducts = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
        if (products == null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View();
        }

        updateProducts.Title = products.Title;
        updateProducts.Price = products.Price;
        updateProducts.OldPrice = products.OldPrice;
        updateProducts.Rating = products.Rating;
        updateProducts.SKU = products.SKU;
        updateProducts.Description = products.Description;
        updateProducts.Features = products.Features;
        updateProducts.Material = products.Material;
        updateProducts.Manufacturer = products.Manufacturer;
        updateProducts.ClaimedSize = products.ClaimedSize;
        updateProducts.RecommendedUse = products.RecommendedUse;
        updateProducts.CategoryId = products.CategoryId;
        updateProducts.BrandId = products.BrandId;


        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
