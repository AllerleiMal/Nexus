using BlogApp.Context;

namespace BlogApp.Controllers;

public class PostController
{
    private BlogContext _context;

    public PostController(BlogContext context)
    {
        _context = context;
    }
}