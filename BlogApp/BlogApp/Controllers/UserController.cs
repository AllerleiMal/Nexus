using BlogApp.Context;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class UserController : Controller
{
    private BlogContext _context;

    public UserController(BlogContext context)
    {
        _context = context;
    }

    [Authorize]
    public IActionResult Profile(int? id)
    {
        var userTask = _context.Users
            .Where(user => user.Id == id)
            .Include(user => user.Admin)
            .Include(user => user.Blogs)
            .ThenInclude(blog => blog.Posts)
            .ThenInclude(post => post.Tags)
            .Include(user => user.FollowedAuthors)
            .ThenInclude(subscription => subscription.Author)
            .AsSplitQuery()
            .FirstOrDefaultAsync();

        return View(userTask.Result);
    }

    [Authorize]
    public IActionResult Settings(int? id)
    {
        var authorizedUserEmailClaim = User.Claims.FirstOrDefault(claim => claim.Type == "email");

        if (authorizedUserEmailClaim is null)
        {
            return RedirectToAction("AccessDenied", "Entry");
        }

        var targetUserTask = _context.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (targetUserTask.Result is null)
        {
            return View(targetUserTask.Result);
        }

        if (!targetUserTask.Result.Email.Equals(authorizedUserEmailClaim.Value))
        {
            return RedirectToAction("AccessDenied", "Entry");
        }

        return View(targetUserTask.Result);
    }

    private User? GetCurrentUser()
    {
        return _context.Users.Where(user =>
            user.Username.Equals(User.Identity.Name)).Include(user => user.FollowedAuthors).FirstOrDefault();
    }
    
    [Authorize]
    public async Task<IActionResult> Subscribe(int? id)
    {
        var currentUser = GetCurrentUser();
        var targetUser = _context.Users.FirstOrDefault(user => user.Id == id);
        if (targetUser is null)
        {
            return RedirectToAction("Index", "Home");
        }

        if (currentUser.FollowedAuthors.FirstOrDefault(sub => sub.AuthorId == id) is null)
        {
            currentUser.FollowedAuthors.Add(new Subscription
            {
                User = currentUser,
                Author = targetUser,
                Date = DateTime.Today
            });
            
            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();
        }
        

        return RedirectToAction("Profile", new {id});
    }
}