using EFCodeTest.Data;
using EFCodeTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCodeTest.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    private readonly EDbContext _db;

    public BlogController(EDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public Blog? Get(int id)
    {
        var b = _db.BlogQueryable.FirstOrDefault();


        var query = _db.Blog.Where(t => t.BlogId == 2);
        query = query.Where(t => t.Url == "url");
        var result = query.FirstOrDefault();


        Blog? blog = _db.Blog.Include(b => b.Posts).Where(t => t.BlogId == id).FirstOrDefault();
        return blog;
    }

    [HttpPost]
    public void Post(Blog blog)
    {
        _db.Blog.Add(blog);
        _db.SaveChanges();
    }

    [HttpPost("Post2")]
    public void Post2(Blog blog)
    {
        _db.Add(blog);
        _db.SaveChanges();
    }

    [HttpDelete]
    public bool Delete(int id)
    {
        Blog? blog = _db.Blog.Find(id) ?? null;
        if (blog == null) return false;
        _db.Blog.Remove(blog);
        _db.SaveChanges();
        return true;
    }

    [HttpDelete]
    [Route("DeleteCascade")]
    public bool DeleteCascade(int id)
    {
        Blog? blog = _db.Blog.Include(t => t.Posts).FirstOrDefault(t => t.BlogId == id) ?? null;
        if (blog == null) return false;
        _db.Blog.Remove(blog);
        _db.SaveChanges();
        return true;
    }

    [HttpGet]
    [Route("query")]
    public object GetByQuery()
    {
        //var query = from blog in _db.Set<Blog>()
        //            join post in _db.Set<Post>()
        //                on blog.BlogId equals post.BlogId
        //            select new { blog.Url, post.Title};

        //var blogs = query.ToList();

        var query = from b in _db.Set<Blog>()
                    from p in _db.Set<Post>()
                    .Where(p => b.BlogId == p.BlogId).DefaultIfEmpty()
                    select new { b, p };
        var blogs = query.ToList();

        return blogs;
    }

    [HttpGet]
    [Route("lambda")]
    public object GetByLambda()
    {
        var blogs = _db.Set<Blog>()
            .GroupJoin(_db.Set<Post>(),
                        b => b.BlogId,
                        p => p.BlogId, (b, p) => new { b, p })
            .SelectMany(n => n.p.DefaultIfEmpty(), 
                        (n, p) => new { n.b.Url, p!.Title })
            .ToList();

        var ls = _db.Set<Blog>()
            .SelectMany(b => _db.Set<Post>().Where(p => b.BlogId == p.BlogId).DefaultIfEmpty(), 
                        (b, ps) => new { b.Url, ps.Title })
            .ToList();

        return blogs;
    }
}
