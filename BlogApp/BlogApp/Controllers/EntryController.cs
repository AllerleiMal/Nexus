using System.Security.Claims;
using BlogApp.Context;
using BlogApp.Filters;
using BlogApp.Models;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class EntryController : Controller
{
    private BlogContext _context;

    public EntryController(BlogContext context)
    {
        _context = context;
    }
    
    [ViewLayout("_NoHeaderAndFooterLayout")]
    public IActionResult AccessDenied()
    {
        ViewData["AccessDenied"] = true;
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login");
    }

    [ViewLayout("_NoHeaderAndFooterLayout")]
    public async Task<IActionResult> Login()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ViewLayout("_NoHeaderAndFooterLayout")]
    public async Task<IActionResult> Login(LoginViewModel userData)
    {
        var targetUser = _context.Users.Include(user => user.Admin).FirstOrDefault(user =>
            user.Username.Equals(userData.Username) && user.Password.Equals(userData.Password));
        if (targetUser is null)
        {
            ModelState.AddModelError("Password", "Password or Username is incorrect");
        }
        
        
        if (ModelState.IsValid)
        {

            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, targetUser.Username),
                new (ClaimsIdentity.DefaultRoleClaimType, targetUser.Admin is null ? Role.User : Role.Admin)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            
            string returnUrl = HttpContext.Request.Query["returnUrl"];
            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);   
            }
            else
            {
                return RedirectToAction("Index", "Home");   
            }
        }
        
        return View(userData);
    }

    [ViewLayout("_NoHeaderAndFooterLayout")]
    public async Task<IActionResult> Register()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ViewLayout("_NoHeaderAndFooterLayout")]
    public async Task<IActionResult> Register(RegisterViewModel userData)
    {
        var userWithSameEmailExists = await _context.Users.AnyAsync(user => user.Email.Equals(userData.Email));

        var userWithSameUsernameExists = await _context.Users.AnyAsync(user => user.Username.Equals(userData.Username));

        if (userWithSameEmailExists)
        {
            ModelState.AddModelError("Email", "This Email is already in use");
        }
        
        if (userWithSameUsernameExists)
        {
            ModelState.AddModelError("Username", "This Username is already in use");
        }
        

        if (ModelState.IsValid)
        {
            await _context.Users.AddAsync(new User
            {
                Username = userData.Username,
                Email = userData.Email,
                Password = userData.Password
            });
            
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, userData.Username),
                new (ClaimsIdentity.DefaultRoleClaimType, Role.User)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            
            return RedirectToAction("Index", "Home");
        }

        return View(userData);
    }
}