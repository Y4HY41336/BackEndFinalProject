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
        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            ModelState.AddModelError("", "Please confirm your email address");
            return View(model);
        }

        var signInResault = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
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

    public async Task<IActionResult> ConfirmEmail(ConfirmEmailViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return NotFound();

        if (await _userManager.IsEmailConfirmedAsync(user))
            return BadRequest();

        IdentityResult identityResult = await _userManager.ConfirmEmailAsync(user, model.Token);
        if (identityResult.Succeeded)
        {
            TempData["ConfirmationMessage"] = "Your email successfully confirmed";
            return RedirectToAction(nameof(Login));
        }

        return BadRequest();
    }



}
