using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels;

public class LoginViewModel
{
    [DisplayName("Username")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name length must be from 2 to 50 symbols")]
    public string Username { get; set; }
    
    [DisplayName("Password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage =
            "Password must contain minimum eight characters, at least one letter, one number and one special character")]
    public string Password { get; set; }
}