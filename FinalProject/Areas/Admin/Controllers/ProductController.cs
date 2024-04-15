//using FinalProject.Context;
//using FinalProject.Models;
//using FinalProject.ViewModel;
//using Microsoft.AspNetCore.Mvc;
//using System.Drawing;

//namespace FinalProject.Areas.Admin.Controllers;
//[Area("Admin")]

//public class ProductController : Controller
//{
//    private readonly AppDbContext _context;
//    private readonly IWebHostEnvironment _webHostEnvironment;
//    public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
//    {
//        _context = context;
//        _webHostEnvironment = webHostEnvironment;
//    }

//    public IActionResult Index()
//    {
//        var products = _context.Products.ToList();


//        return View(products);
//    }
//    public IActionResult Create()
//    {
//        return View();
//    }
//    [HttpPost]
//    public async Task<IActionResult> Create(ProductViewModel products)
//    {
//        foreach (var Image in images)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View();
//            }
//            if (!products.Image.CheckFileSize(3000))
//            {
//                ModelState.AddModelError("Image", "Too Big!");
//                return View();
//            }
//            if (!products.Image.CheckFileType("image/"))
//            {
//                ModelState.AddModelError("Image", "sekil olsun");
//                return View();
//            }
//            string fileName = $"{Guid.NewGuid()}-{products.Image.FileName}";
//            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", fileName);
//            FileStream stream = new FileStream(path, FileMode.Create);
//            await products.Image.CopyToAsync(stream);
//            stream.Dispose();

//            Product newproduct = new()
//            {
//                Title = products.Title,
//                Description = products.Description,
//                Image = fileName
//            };
//        }
//        _context.Products.Add(newproduct);
//        _context.SaveChanges();
//        return RedirectToAction("Index");
//    }
//    //public async Task<IActionResult> Delete(int id)
//    //{
//    //    var slider = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
//    //    if (slider == null)
//    //    {
//    //        return NotFound();
//    //    }
//    //    return View(slider);
//    //}

//    //[HttpPost]
//    //[ActionName("Delete")]
//    //public async Task<IActionResult> DeleteSlider(int id)
//    //{
//    //    var slider = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);
//    //    if (slider == null)
//    //    {
//    //        return NotFound();
//    //    }
//    //    string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "website-images", slider.Image);
//    //    if (System.IO.File.Exists(path))
//    //    {
//    //        System.IO.File.Delete(path);
//    //    }
//    //    _context.Remove(slider);
//    //    await _context.SaveChangesAsync();
//    //    return RedirectToAction("Index");
//    //}
//}
