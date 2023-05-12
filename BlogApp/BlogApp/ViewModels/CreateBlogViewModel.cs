using System.ComponentModel.DataAnnotations;
using BlogApp.DataAttributes;

namespace BlogApp.ViewModels;

public class CreateBlogViewModel
{
    [Required(ErrorMessage = "Blog title is required.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "Title length should be from 2 to 50 characters")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Blog description is required.")]
    [StringLength(300, MinimumLength = 2)] 
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Please select a preview image.")]
    [DataType(DataType.Upload)]
    [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".gif" })]
    [MaxFileSize(10 * 1024 * 1024)]
    public IFormFile PreviewImage { get; set; } = null!;
}