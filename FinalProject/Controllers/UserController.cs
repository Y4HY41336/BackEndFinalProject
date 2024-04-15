using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

public class UserController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(AppUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        AppUser appUser = new AppUser()
        {
            Fullname = model.UserName,
            Email = model.Email,
            UserName = model.UserName,
            IsActive = true
        };

        IdentityResult identityResult = await _userManager.CreateAsync(appUser, model.Password);
        if (!identityResult.Succeeded)
        {
            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();
        }
        var user = model.UserName;
        var signInResault = await _signInManager.PasswordSignInAsync(user, model.Password,false, false);
        return RedirectToAction("Index", "Home");
    }


}
