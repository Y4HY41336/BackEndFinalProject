using FinalProject.Areas.Admin.ViewModel;
using FinalProject.Context;
using FinalProject.Helpers.Extencions;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace FinalProject.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin,Moderator")]
public class BrandController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public BrandController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var brand = _context.Brands.ToList();

        return View(brand);
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BrandViewModel brand)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (brand.Image.CheckFileSize(3000))
        {
            ModelState.AddModelError("Image", "Too Big!");
            return View();
        }
        if (!brand.Image.CheckFileType("image/"))
        {
            ModelState.AddModelError("Image", "sekil olsun");
            return View();
        }
        string fileName = $"{Guid.NewGuid()}-{brand.Image.FileName}";
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "brands", fileName);
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            await brand.Image.CopyToAsync(stream);
        }
        Brand newbrand = new()
        {
            BrandName = brand.BrandName,
            Image = fileName,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        await _context.Brands.AddAsync(newbrand);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {

        var brand = await _context.Brands.FirstOrDefaultAsync(b=> b.Id == id && !b.isDeleted);;
        if (brand == null)
        {
            return NotFound();
        }
        return View(brand);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteBrand(int id)
    {

        var brand = await _context.Brands.FirstOrDefaultAsync(b=> b.Id == id && !b.isDeleted);;
        if (brand == null)
        {
            return NotFound();
        }
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "brands", brand.Image);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }

        brand.isDeleted = true;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Detail(int id)
    {

        var brand = await _context.Brands.FirstOrDefaultAsync(b=> b.Id == id && !b.isDeleted);;
        if (brand == null)
        {
            return NotFound();
        }
        return View(brand);
    }
    public async Task<IActionResult> Update(int id)
    {
        var brand = await _context.Brands.AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id && !b.isDeleted);
        if (brand == null)
            return NotFound();

        BrandUpdateViewModel model = new()
        {
            BrandName = brand.BrandName,
        };

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, BrandUpdateViewModel brand)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var updateBrand = await _context.Brands.FirstOrDefaultAsync(b=> b.Id == id && !b.isDeleted);
        if (updateBrand == null)
        {
            return NotFound();
        }

        if (brand.Image != null)
        {
            if (brand.Image.CheckFileSize(3000))
            {
                ModelState.AddModelError("Image", "Image size is too big");
                return View();
            }

            if (!brand.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Only images are allowed");
                return View();
            }

            string basePath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "brands");
            string path = Path.Combine(basePath, updateBrand.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            string fileName = $"{Guid.NewGuid()}-{brand.Image.FileName}";
            path = Path.Combine(basePath, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await brand.Image.CopyToAsync(stream);
            }
            updateBrand.Image = fileName;
        }


        updateBrand.BrandName = brand.BrandName;
        updateBrand.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
