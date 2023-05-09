using BlogApp.Context;
using BlogApp.Filters;
using BlogApp.Models;
using BlogApp.ViewModels;
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
    public IActionResult Login()
    {
        return View();
    }

    [ViewLayout("_NoHeaderAndFooterLayout")]
    public IActionResult Register()
    {
        return View();
    }

    [ViewLayout("_NoHeaderAndFooterLayout")]
    public IActionResult AccessDenied()
    {
        ViewData["AccessDenied"] = true;
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
            Console.WriteLine(
                $"{userData.Email}, {userData.Password}, {userData.PasswordConfirm}, {userData.Username}");
            await _context.Users.AddAsync(new User
            {
                Username = userData.Username,
                Email = userData.Email,
                Password = userData.Password
            });
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        return View(userData);
    }
}