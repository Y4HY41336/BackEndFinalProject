using FinalProject.Areas.Admin.ViewModel;
using FinalProject.Context;
using FinalProject.Helpers.Extencions;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;

namespace FinalProject.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin,Moderator")]
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel products)
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (products.PosterImage.CheckFileSize(3000))
        {
            ModelState.AddModelError("PosterImage", "Image size is too big");
            return View();
        }
        if (!products.PosterImage.CheckFileType("image/"))
        {
            ModelState.AddModelError("PosterImage", "Only images are allowed");
            return View();
        }
        string fileName = $"{Guid.NewGuid()}-{products.PosterImage.FileName}";
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "shop", fileName);
        FileStream stream = new FileStream(path, FileMode.Create);
        await products.PosterImage.CopyToAsync(stream);
        stream.Dispose();
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
            BrandId = products.BrandId,
            CategoryId = products.CategoryId,
            PosterImage = fileName,
            isStocked = true,
            isDeleted = false,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        await _context.Products.AddAsync(newproduct);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var products = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && !p.isDeleted);
        if (products == null)
        {
            return NotFound();
        }
        return View(products);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.isDeleted);
        if (products == null)
        {
            return NotFound();
        }

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "shop", products.PosterImage);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }

        products.isDeleted = true;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        var products = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && !p.isDeleted);
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
        var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.isDeleted);
        if (products == null)
        {
            return NotFound();
        }
        ProductUpdateViewModel model = new()
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
            BrandId = products.BrandId,
            CategoryId = products.CategoryId,
            isStocked = true,
        };

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, ProductUpdateViewModel products)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        ViewBag.Categories = await _context.Categories.ToListAsync();
        ViewBag.Brands = await _context.Brands.ToListAsync();
        var updateProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.isDeleted);
        if (products == null)
        {
            return NotFound();
        }

        if (products.PosterImage != null)
        {
            if (products.PosterImage.CheckFileSize(3000))
            {
                ModelState.AddModelError("Image", "Image size is too big");
                return View();
            }

            if (!products.PosterImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Only images are allowed");
                return View();
            }

            string basePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "shop");
            string path = Path.Combine(basePath, updateProduct.PosterImage);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            string fileName = $"{Guid.NewGuid()}-{products.PosterImage.FileName}";
            path = Path.Combine(basePath, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await products.PosterImage.CopyToAsync(stream);
            }
            updateProduct.PosterImage = fileName;
        }

        updateProduct.Title = products.Title;
        updateProduct.Price = products.Price;
        updateProduct.OldPrice = products.OldPrice;
        updateProduct.Rating = products.Rating;
        updateProduct.SKU = products.SKU;
        updateProduct.Description = products.Description;
        updateProduct.Features = products.Features;
        updateProduct.Material = products.Material;
        updateProduct.Manufacturer = products.Manufacturer;
        updateProduct.ClaimedSize = products.ClaimedSize;
        updateProduct.RecommendedUse = products.RecommendedUse;
        updateProduct.BrandId = products.BrandId;
        updateProduct.CategoryId = products.CategoryId;
        updateProduct.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
