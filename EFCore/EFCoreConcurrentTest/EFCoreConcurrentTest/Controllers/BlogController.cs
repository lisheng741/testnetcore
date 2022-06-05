using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreConcurrentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private static readonly object _lock = new object();
        private readonly EDbContext _db;

        public BlogController(EDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("delete")]
        public void Delete()
        {
            var blogs = _db.Blog.Where(t => t.Url == "set1" || t.Url == "set2").ToList();
            _db.Blog.RemoveRange(blogs);
            _db.SaveChanges();
        }

        [HttpGet]
        [Route("set1")]
        public async Task Set1()
        {
            lock (_lock) { }
            Blog blog = new Blog() { Url = "set1" };
            _db.Add(blog);

            await Task.Delay(5000);
            
            _db.SaveChanges();
        }

        [HttpGet]
        [Route("set2")]
        public void Set2()
        {
            lock (_lock)
            {
                Blog? blog1 = _db.Blog.Where(t => t.Url == "set1").FirstOrDefault();
                if (blog1 != null) return;
                Blog blog = new Blog() { Url = "set2" };
                _db.Add(blog);
                _db.SaveChanges();
            }
        }

        [HttpGet]
        [Route("set3")]
        public void Set3()
        {
            lock (_lock)
            {
                Task.Delay(5000).Wait();
                Blog blog = new Blog() { Url = "set1" };
                _db.Add(blog);
                _db.SaveChanges();
            }
        }
    }
}
