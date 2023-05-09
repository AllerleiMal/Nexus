using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models;

public class Notification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Description { get; set; }

    public int UserId { get; set; }

    public int TypeId { get; set; }

    public virtual NotificationType Type { get; set; } = null!;
    
    public virtual User User { get; set; } = null!;
}
