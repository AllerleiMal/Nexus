using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public byte[]? PreviewImage { get; set; }

    public int? BlogId { get; set; }

    public bool ModerationPassed { get; set; }
    
    [StringLength(300)]
    public string? Description { get; set; }

    public virtual Blog? Blog { get; set; }

    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
