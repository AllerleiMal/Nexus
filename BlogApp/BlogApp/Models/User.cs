using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class User
{
    [Key] 
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name length must be from 2 to 50 symbols")]
    public string Username { get; set; } = null!;

    [StringLength(50)]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Wrong email format")]
    public string Email { get; set; } = null!;

    [StringLength(50, MinimumLength = 2, ErrorMessage = "First name length must be from 2 to 50 symbols")]
    public string? FirstName { get; set; }

    [StringLength(50, MinimumLength = 2, ErrorMessage = "Second name length must be from 2 to 50 symbols")]
    public string? SecondName { get; set; }

    public byte[]? ProfileImage { get; set; }

    public byte[]? ProfileHeader { get; set; }

    public string? About { get; set; }

    [DataType(DataType.Date)] 
    public DateTime RegistrationDate { get; set; }

    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$",
        ErrorMessage =
            "Password must contain minimum eight characters, at least one letter, one number and one special character")]
    public string? Password { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Subscription> SubscriptionAuthors { get; set; } = new List<Subscription>();

    public virtual ICollection<Subscription> SubscriptionUsers { get; set; } = new List<Subscription>();
}