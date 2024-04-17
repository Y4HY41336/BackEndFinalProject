using FinalProject.Areas.Admin.ViewModel;
using FinalProject.Context;
using FinalProject.Helpers;
using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace FinalProject.Areas.Admin.Controllers;
[Area("Admin")]

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
        FileStream stream = new FileStream(path, FileMode.Create);
        await brand.Image.CopyToAsync(stream);
        stream.Dispose();
        Brand newbrand = new()
        {
            BrandName = brand.BrandName,
            Image = fileName
        };
        await _context.Brands.AddAsync(newbrand);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {

        var brand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == id);
        if (brand == null)
        {
            return NotFound();
        }
        return View(brand);
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteProduct(int id)
    {

        var brand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == id);
        if (brand == null)
        {
            return NotFound();
        }
        _context.Remove(brand);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Detail(int id)
    {

        var brand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == id);
        if (brand == null)
        {
            return NotFound();
        }
        return View(brand);
    }
    public async Task<IActionResult> Update(int id)
    {
        var brand = await _context.Brands.FirstOrDefaultAsync(c => c.Id == id);
        if (brand == null)
        {
            return NotFound();
        }
        return View(brand);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, BrandViewModel brand)
    {
        if (brand == null)
        {
            return NotFound();
        }
        var updatecategory = await _context.Brands.FirstOrDefaultAsync(c => c.Id == id);
        if (!ModelState.IsValid)
        {
            return View();
        }

        updatecategory.BrandName = brand.BrandName;

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
