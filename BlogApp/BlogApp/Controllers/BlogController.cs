using BlogApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class BlogController: Controller
{
    private BlogContext _context;

    public BlogController(BlogContext context)
    {
        _context = context;
    }

    public IActionResult Details(int? id)
    {
        var blog = _context.Blogs
            .Where(blog => blog.Id == id)
            .Include(blog => blog.Posts)
            .ThenInclude(post => post.Tags)
            .AsSplitQuery()
            .FirstOrDefault();

        return View(blog);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    public IActionResult Post(int? id)
    {
        var post = _context.Posts
            .Where(post => post.Id == id)
            .Include(post => post.Blog)
            .ThenInclude(blog => blog.User)
            .Include(post => post.Tags)
            .AsSplitQuery()
            .FirstOrDefault();
        
        return View(post);
    }
}