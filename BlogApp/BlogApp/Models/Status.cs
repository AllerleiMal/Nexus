namespace BlogApp.Models;

public class Status
{
    public const string Accepted = "Accepted";
    public const string Declined = "Declined";
    public const string Deleted = "Deleted";
    public const string Pending = "Pending";
    
    public int Id { get; set; }
    public string? Title { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}