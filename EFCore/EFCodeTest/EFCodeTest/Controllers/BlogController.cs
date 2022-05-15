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


        var query = _db.Blog.Where(t => t.BlogId == 1);
        query = query.Where(t => t.Url == "string");
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

    [HttpDelete]
    public bool Delete(int id)
    {
        Blog? blog = _db.Blog.Find(id) ?? null;
        if (blog == null) return false;
        _db.Blog.Remove(blog);
        _db.SaveChanges();
        return true;
    }
}
