using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels;

public class RegisterViewModel
{
    [Required]
    [DisplayName("Username")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Username length must be from 2 to 50 symbols")]
    public string Username { get; set; }

    [Required]
    [DisplayName("Email")]
    [StringLength(50)]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Wrong email format")]
    public string Email { get; set; }
    
    [Required]
    [DisplayName("Password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage =
            "Password must contain minimum eight characters, at least one letter, one number and one special character")]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password", ErrorMessage = "Passwords are not the same")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string PasswordConfirm { get; set; }
    
    [Required(ErrorMessage = "Please accept the terms and condition.")]
    [Display(Name = "AgreedWithTerms")]
    public bool AgreedWithTerms { get; set; }
}