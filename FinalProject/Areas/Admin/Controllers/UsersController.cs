using FinalProject.Areas.Admin.ViewModel;
using FinalProject.Context;
using FinalProject.Helpers.Enums;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Moderator")]
public class UsersController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<AppUser> _userManager;

    public UsersController(AppDbContext context, IWebHostEnvironment webHostEnvironment, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _roleManager = roleManager;
        _userManager = userManager;
    }


    public async Task<IActionResult> Index()
    {
        var users = await _userManager.Users.ToListAsync();
        var roles = await _roleManager.Roles.ToListAsync();
        var UserRoles = new List<UserViewModel>();

        foreach (var user in users)
        {
            if (user.UserName != User.Identity.Name)
            {
                var rolesForUser = await _userManager.GetRolesAsync(user);
                UserRoles.Add(new UserViewModel
                {
                    User = user,
                    Roles = rolesForUser,
                });

            }
            
        }
        return View(UserRoles);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Ban(string id)
    {
        var user = await _userManager.FindByNameAsync(id);
 
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ActionName("Ban")]
    public async Task<IActionResult> BanOrUnbanUser(string id)
    {
        var user = await _userManager.FindByNameAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        if (user.IsActive)
        {
            user.IsActive = false;
        }
        else
        {
            user.IsActive = true;
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Users");

    }

    //public async Task<IActionResult> UpdateRole(string id)
    //{
    //    ViewBag.AllRoles = await _roleManager.Roles.ToListAsync();

    //    var user = await _userManager.FindByNameAsync(id);
    //    var roles = await _userManager.GetRolesAsync(user);

    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    UserViewModel model = new()
    //    {
    //        User = user,
    //        Roles = roles
    //    };

    //    return View(model);
    //}


    //[HttpPost]
    //public async Task<IActionResult> UpdateRole(string id, UserUpdateViewModel model)
    //{
    //    var user = await _userManager.FindByNameAsync(id);
    //    UserViewModel users = new();

    //    await _context.SaveChangesAsync();

    //    return RedirectToAction(nameof(Index));
    //}
}
