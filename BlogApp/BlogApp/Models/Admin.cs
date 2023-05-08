using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class Admin
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Reputation { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
