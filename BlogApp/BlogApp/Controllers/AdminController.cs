using BlogApp.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class AdminController : Controller
{
    private BlogContext _context;

    public AdminController(BlogContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UserList()
    {
        return View(await _context.Users.ToListAsync());
    }

    public IActionResult Requests()
    {
        var postsPendingReview = _context.Posts
            .Include(post => post.Blog)
            .Include(post => post.Tags)
            .Where(post => post.ModerationPassed == false)
            .ToListAsync();
        return View(postsPendingReview.Result);
    }
}