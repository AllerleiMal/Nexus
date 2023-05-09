using System.ComponentModel.DataAnnotations;
using BlogApp.DataAttributes;

namespace BlogApp.ViewModels;

public class CreatePostViewModel
{
    [DataType(DataType.Upload)]
    [MaxFileSize(5* 1024 * 1024)]
    [AllowedExtensions(new string[] { ".jpg", ".png" })]
    public IFormFile PreviewImage { get; set; }
}