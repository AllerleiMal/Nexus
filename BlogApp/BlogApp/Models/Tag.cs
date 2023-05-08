using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class Tag
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(30, MinimumLength = 2)]
    [Required]
    public string Title { get; set; } = null!;

    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
