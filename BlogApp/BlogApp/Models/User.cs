using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models;

public class User
{
    [Key] 
    [DisplayName("ID")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DisplayName("Username")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name length must be from 2 to 50 symbols")]
    public string Username { get; set; } = null!;

    [DisplayName("Email")]
    [StringLength(50)]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Wrong email format")]
    public string Email { get; set; } = null!;

    [DisplayName("First name")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name length must be from 2 to 50 symbols")]
    public string? FirstName { get; set; }

    [DisplayName("Second name")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Second name length must be from 2 to 50 symbols")]
    public string? SecondName { get; set; }

    [DisplayName("Profile image")]
    public byte[]? ProfileImage { get; set; }

    [DisplayName("Profile header")]
    public byte[]? ProfileHeader { get; set; }

    [DisplayName("About")]
    public string? About { get; set; }
    
    [DisplayName("Registration date")]
    [DataType(DataType.Date)] 
    public DateTime RegistrationDate { get; set; }

    [DisplayName("Password")]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage =
            "Password must contain minimum eight characters, at least one letter, one number and one special character")]
    public string Password { get; set; } = null!;

    public virtual Admin? Admin { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Subscription> Subscribers { get; set; } = new List<Subscription>();

    public virtual ICollection<Subscription> FollowedAuthors { get; set; } = new List<Subscription>();
}