using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class Blog
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(60)]
    public string Title { get; set; } = null!;
    
    [StringLength(300)]
    public string? Description { get; set; }

    public byte[]? PreviewImage { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual User User { get; set; } = null!;
}
