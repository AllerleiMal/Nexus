using System.Drawing;

namespace BlogApp.Validators;

public static class ImageIntegrityValidator
{
    public static async Task<bool> CheckImageIntegrity(IFormFile? file)
    {
        if (file is null || file.Length == 0)
        {
            return false;
        }

        try
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            using var img = Image.FromStream(memoryStream);
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}