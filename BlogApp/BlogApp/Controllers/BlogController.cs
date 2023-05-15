using BlogApp.Context;
using BlogApp.Models;
using BlogApp.Validators;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    public IActionResult Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        
        var blog = _context.Blogs
            .Where(blog => blog.Id == id)
            .Include(blog => blog.User)
            .Include(blog => blog.Posts)
            .ThenInclude(post => post.Tags)
            .AsSplitQuery()
            .FirstOrDefault();

        return View(blog);
    }

    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateBlogViewModel blogData)
    {
        var currentUser = GetCurrentUser();

        if (currentUser is null)
        {
            return RedirectToAction("Login", "Entry");
        }
        
        if (ModelState.IsValid)
        {
            if (!FileSignatureValidator.IsFileValid(blogData.PreviewImage))
            {
                ModelState.AddModelError("PreviewImage", "File signature is not allowed");
            }
        }
        
        if (ModelState.IsValid)
        {
            if (!ImageIntegrityValidator.CheckImageIntegrity(blogData.PreviewImage).Result)
            {
                ModelState.AddModelError("PreviewImage", "Image is corrupted");
            }
        }

        if (ModelState.IsValid)
        {
            var binaryImage = GetBytesFromFile(blogData.PreviewImage);
            Blog blog = new Blog
            {
                Description = blogData.Description,
                Title = blogData.Title,
                PreviewImage = binaryImage.Result,
                User = currentUser
            };

            _context.Blogs.Add(blog);
            _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        else
        {
            return View(blogData);   
        }
    }
    
    private async Task<byte[]> GetBytesFromFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
    
    [Authorize]
    public IActionResult Post(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }
        
        var post = _context.Posts
            .Where(post => post.Id == id)
            .Include(post => post.Blog)
            .ThenInclude(blog => blog.User)
            .Include(post => post.Tags)
            .AsSplitQuery()
            .FirstOrDefault();
        
        return View(post);
    }
    
    private User? GetCurrentUser()
    {
        return _context.Users.FirstOrDefault(user => user.Username.Equals(User.Identity.Name));
    }
}