using System.Diagnostics;
using System.Drawing;
using System.Security.Claims;
using BlogApp.Context;
using BlogApp.DataAttributes;
using BlogApp.Models;
using BlogApp.Validators;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers;

public class PostController : Controller
{
    private BlogContext _context;

    public PostController(BlogContext context)
    {
        _context = context;
    }

    [Authorize]
    public IActionResult Create()
    {
        var currentUser = GetCurrentUser();

        if (currentUser is null)
        {
            return RedirectToAction("Login", "Entry");
        }

        var userBlogList = Models.User.GetUserBlogsItems(currentUser);
        
        var postVm = new CreatePostViewModel
        {
            UserBlogs = userBlogList
        };

        if (!userBlogList.Any())
        {
            ModelState.AddModelError("BlogId", "There is no blogs. Create a new one first");
        }
        
        return View(postVm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreatePostViewModel postData)
    {
        // Stopwatch timer = new Stopwatch();
        // timer.Start();
        // var imageIsValid = FileSignatureValidator.IsFileValid(postData.PreviewImage);
        // Console.WriteLine($"{timer.ElapsedTicks} time for signature");
        // timer.Restart();
        // imageIsValid = CheckImage(postData.PreviewImage).Result;
        // Console.WriteLine($"{timer.ElapsedTicks} time for image creation");

        var currentUser = GetCurrentUser();

        if (currentUser is null)
        {
            return RedirectToAction("Login", "Entry");
        }

        postData.UserBlogs = Models.User.GetUserBlogsItems(currentUser);

        if (ModelState.IsValid)
        {
            if (!FileSignatureValidator.IsFileValid(postData.PreviewImage))
            {
                ModelState.AddModelError("PreviewImage", "File signature is not allowed");
            }
        }
        
        if (ModelState.IsValid)
        {
            if (!ImageIntegrityValidator.CheckImageIntegrity(postData.PreviewImage).Result)
            {
                ModelState.AddModelError("PreviewImage", "Image is corrupted");
            }
        }

        if (ModelState.IsValid)
        {
            var binaryImage = GetBytesFromFile(postData.PreviewImage);
            Post post = new Post
            {
                BlogId = postData.BlogId,
                Content = postData.Content,
                Description = postData.Description,
                Title = postData.Title,
                Tags = ParseKeywordsToTags(postData.KeyWords),
                PreviewImage = binaryImage.Result
            };

           _context.Posts.Add(post);
           _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        else
        {
            return View(postData);   
        }
    }

    private User? GetCurrentUser()
    {
        return _context.Users.Where(user =>
            user.Username.Equals(User.Identity.Name)).Include(user => user.Blogs).FirstOrDefault();
    }

    private async Task<byte[]> GetBytesFromFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    private List<Tag> ParseKeywordsToTags(string keywords)
    {
        string[] splitKeys = keywords.Trim().Replace("#", "").ToLower().Split(" ").Distinct().ToArray();
        var existedTags = _context.Tags.Select(tag => tag).Where(tag => splitKeys.Contains(tag.Title)).ToList();

        List<Tag> postTags = new List<Tag>();
        foreach (var key in splitKeys)
        {
            var tag = existedTags.FirstOrDefault(tag => tag.Title == key);
            if (tag is null)
            {
                var entry = _context.Tags.Add(new Tag
                {
                    Title = key
                });
                
                postTags.Add(entry.Entity);
            }
            else
            {
                postTags.Add(tag);
            }
        }

        _context.SaveChangesAsync();

        return postTags;
    }
}