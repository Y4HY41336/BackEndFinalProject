using FinalProject.Models;
using FinalProject.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

public class AuthController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(model.UsernameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username/Email or Password incorrect");
                return View();
            }
        }

        var signInResault = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
        if (!signInResault.Succeeded)
        {
            ModelState.AddModelError("", "Username/Email or Password incorrect");
            return View();
        }

        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Logout()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return BadRequest();
        }
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> ForgotPassword()
    {
        return View();
    }
}
