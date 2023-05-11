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
            .Include(blog => blog.Posts)
            .ThenInclude(post => post.Tags).FirstOrDefault(blog => blog.Id == id);

        return View(blog);
    }
    
    public IActionResult Post(int? id)
    {
        var post = _context.Posts
            .Include(post => post.Blog)
            .Include(post => post.Tags)
            .FirstOrDefault(post => post.Id == id);
        return View(post);
    }
}