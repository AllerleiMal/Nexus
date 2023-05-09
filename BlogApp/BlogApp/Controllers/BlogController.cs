using BlogApp.Context;

namespace BlogApp.Controllers;

public class BlogController
{
    private BlogContext _context;

    public BlogController(BlogContext context)
    {
        _context = context;
    }
}