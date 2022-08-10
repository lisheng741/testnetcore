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

    [HttpPost]
    public void SetPerson(int n, Guid[] ids)
    {
        var person = new Person()
        {
            Name = $"测试{n}"
        };
        var blogs = _context.Set<Blog>().Where(b => ids.Contains(b.Id)).ToList();
        person.Blogs.AddRange(blogs);
        _context.Add(person);
        _context.SaveChanges();
    }

    [HttpGet]
    public int DeletePerson(Guid id)
    {
        var person = _context.Set<Person>().Where(b => b.Id == id).FirstOrDefault();
        if (person == null) return 0;

        _context.Remove(person);
        return _context.SaveChanges();
    }

    [HttpGet]
    public Blog Set(int n)
    {
        var blog = new Blog()
        {
            Name = $"测试{n}",
            Posts = new List<Post>{
                new Post() { Title = $"测试{n}-1" },
                new Post() { Title = $"测试{n}-2" },
                new Post() { Title = $"测试{n}-3" }
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
