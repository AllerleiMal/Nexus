using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models;

public class Subscription
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }

    [Required]
    public int AuthorId { get; set; }

    // [ForeignKey("author_id")]
    public virtual User Author { get; set; } = null!;
    // [ForeignKey("user_id")]
    public virtual User User { get; set; } = null!;
}
