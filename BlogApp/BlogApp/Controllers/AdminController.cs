using BlogApp.Context;
using BlogApp.Models;
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

    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> UserList()
    {
        return View(await _context.Users.Include(user => user.Admin).ToListAsync());
    }

    [Authorize(Roles = Role.Admin)]
    public IActionResult Requests()
    {
        var postsPendingReview = _context.Posts
            .Include(post => post.Status)
            .Where(post => post.Status.Title.Equals(Status.Pending))
            .Include(post => post.Blog)
            .Include(post => post.Tags)
            .ToListAsync();
        return View(postsPendingReview.Result);
    }
    
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var targetUser = _context.Users.FirstOrDefault(user => user.Id == id);
        if (targetUser is not null)
        {
            _context.Users.Remove(targetUser);
            await _context.SaveChangesAsync();

            ViewData["Success"] = $"User {targetUser.Username} deleted successfully";
        }
        else
        {
            ViewData["Failed"] = $"User with id = {id} does not exist";
        }
        return RedirectToAction("UserList");
    }

    private Post? GetPost(int id)
    {
        return _context.Posts.Where(post => post.Id == id).Include(post => post.Status).FirstOrDefault();
    }

    private async Task SavePostUpdates(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> AcceptPost(int id)
    {
        var targetPost = GetPost(id);
        if (targetPost is not null)
        {
            targetPost.Status = _context.Statuses.First(status => status.Title.Equals(Status.Accepted));

            await SavePostUpdates(targetPost);
            
            ViewData["Success"] = $"Post {targetPost.Id} accepted successfully";
        }
        else
        {
            ViewData["Failed"] = $"Post with id = {id} does not exist";
        }
        return RedirectToAction("Requests");
    }
    
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> DeletePost(int id)
    {
        var targetPost = GetPost(id);
        if (targetPost is not null)
        {
            targetPost.Status = _context.Statuses.First(status => status.Title.Equals(Status.Deleted));

            await SavePostUpdates(targetPost);
            
            ViewBag.Success = $"Post {targetPost.Id} deleted successfully";
        }
        else
        {
            ViewBag.Failed = $"Post with id = {id} does not exist";
        }
        return RedirectToAction("Requests");
    }
    
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> DeclinePost(int id)
    {
        var targetPost = GetPost(id);
        if (targetPost is not null)
        {
            targetPost.Status = _context.Statuses.First(status => status.Title.Equals(Status.Declined));

            await SavePostUpdates(targetPost);
            
            ViewData["Success"] = $"Post {targetPost.Id} declined successfully";
        }
        else
        {
            ViewData["Failed"] = $"Post with id = {id} does not exist";
        }
        return RedirectToAction("Requests");
    }
}