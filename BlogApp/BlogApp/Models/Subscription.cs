using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models;

public class Subscription
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public int AuthorId { get; set; }
    
    [DataType(DataType.Date)] 
    public DateTime? Date { get; set; }

    // [ForeignKey("author_id")]
    public virtual User Author { get; set; } = null!;
    // [ForeignKey("user_id")]
    public virtual User User { get; set; } = null!;
}
