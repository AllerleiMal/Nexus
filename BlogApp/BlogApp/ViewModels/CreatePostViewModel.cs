using System.ComponentModel.DataAnnotations;
using BlogApp.DataAttributes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogApp.ViewModels;

public class CreatePostViewModel
{
    public int BlogId { get; set; }
    
    public List<SelectListItem>? UserBlogs { get; set; }
    
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Title length should be from 2 to 50 characters")]
    public string Title { get; set; } = null!;
    
    [Required(ErrorMessage = "Fill the post description.")]
    [StringLength(300)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Please select a preview image.")]
    [DataType(DataType.Upload)]
    [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".gif" })]
    [MaxFileSize(5 * 1024 * 1024)]
    public IFormFile PreviewImage { get; set; } = null!;
    
    public string Content { get; set; } = null!;
    
    public string KeyWords { get; set; }
}