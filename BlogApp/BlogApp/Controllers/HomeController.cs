using System.Diagnostics;
using BlogApp.Context;
using BlogApp.Filters;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private BlogContext _context;

    public HomeController(ILogger<HomeController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _context.Posts.Include(p => p.Blog)
            .Include(p => p.Blog.User)
            .Include(post => post.Tags)
            .ToListAsync();
        return View(data);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Authorize]
    public IActionResult CreatePost()
    {
        return View();
    }

    public async Task<IActionResult> UserList()
    {
        return View(await _context.Users.ToListAsync());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}