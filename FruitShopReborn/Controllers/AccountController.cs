using Core.Entities.Users;
using FruitShopReborn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FruitShopReborn.Controllers;

[Route("/[action]")]
public class AccountController(UserManager<User> userManager,
    SignInManager<User> signInManager) 
    : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl = "/")
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromForm]LoginViewModel model)
    {
        if (!ModelState.IsValid) return View();
        
        var result = await signInManager.PasswordSignInAsync(
            model.Email!, model.Password!,
            model.RememberMe,
            true);
        
        if (result.Succeeded) return LocalRedirect(model.ReturnUrl!);

        ModelState.AddModelError("", "Đăng nhập không thành công. Vui lòng kiểm tra lại email và mật khẩu.");
        
        return View();
    }
    
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Guest");
    }
    
    [HttpGet]
    public IActionResult Register() => View();
    
    [HttpPost]
    public async Task<IActionResult> Register([FromForm]RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View();
        var user = new User {UserName = model.Email, Email = model.Email};
        var result = await userManager.CreateAsync(user, model.Password!);

        if (result.Succeeded)
        {
            TempData["Success"] = "Đăng ký tài khoản thành công.";
            return RedirectToAction("Login", "Account");
        }

        foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
        return View();
    }
    
    [HttpGet]
    public IActionResult AccessDenied() => View();
}