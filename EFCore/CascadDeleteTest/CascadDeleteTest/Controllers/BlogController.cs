using EFCoreMySqlConcurrencyTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CascadDeleteTest.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly SimpleDbContext _context;

    public BlogController(SimpleDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public Blog Set()
    {
        var blog = new Blog()
        {
            Name = "测试",
            Posts = new List<Post>{
                new Post() { Title = "测试111" },
                new Post() { Title = "测试2222" },
                new Post() { Title = "测试33" }
            }
        };
        _context.Add(blog);
        _context.SaveChanges();

        return blog;
    }

    [HttpGet]
    public int Delete(Guid guid)
    {
        var blog = _context.Set<Blog>().Where(b => b.Id == guid).FirstOrDefault();
        if (blog == null) return 0;

        _context.Remove(blog);
        return _context.SaveChanges();
    }


    [HttpGet]
    public int DeleteInclude(Guid guid)
    {
        var blog = _context.Set<Blog>()
            .Include(b => b.Posts)
            .Where(b => b.Id == guid).FirstOrDefault();
        if (blog == null) return 0;

        _context.Remove(blog);
        return _context.SaveChanges();
    }
}
