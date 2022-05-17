// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using RelationshipTest;
using RelationshipTest.Model;

Console.WriteLine("Hello, World!");

EDbContext db = new EDbContext();

db.Blogs.Add(new Blog() { Url = "Test" });
db.Blogs.Add(new Blog()
{
    Url = "test2",
    Posts = new List<Post>()
    {
        new Post() { Title = "Test tile",Content = "Test context" }
    }
});
db.SaveChanges();

var blog = db.Blogs.Include(b => b.Posts) // 关联 Post
    .Where(t => t.BlogId == 2)            // 查出 BlogId = 2 的记录
    .FirstOrDefault();                    // 查出第一条记录

var url = blog.Url;                       // 获取 blog 的 url 属性
var postCount = blog.Posts.Count;         // 获取与 Blog 关联的 Post 的数量
var title = blog.Posts[0].Title;

Console.WriteLine(url);
Console.WriteLine(postCount);
Console.WriteLine(title);

Console.ReadLine();