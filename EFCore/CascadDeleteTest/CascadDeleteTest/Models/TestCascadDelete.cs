using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMySqlConcurrencyTest.Models;

public class Person : EntityBase<Guid>
{
    public string? Name { get; set; }
    public List<Blog> Blogs { get; set; } = new List<Blog>();
}

public class Blog : EntityBase<Guid>
{
    /// <summary>
    /// 博客名
    /// </summary>
    public string? Name { get; set; }

    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }

    /// <summary>
    /// 帖子列表
    /// </summary>
    public virtual List<Post>? Posts { get; set; }

    //public override void ConfigureEntity(ModelBuilder builder)
    //{
    //    builder.Entity<Blog>()
    //        .HasMany(e => e.Posts)
    //        .WithOne(e => e.Blog);

    //    builder.Entity<Post>()
    //        .HasOne(p => p.Blog)
    //        .WithMany(e => e.Posts);
    //}
}

public class Post : EntityBase<Guid>
{
    /// <summary>
    /// 标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 博客
    /// </summary>
    public virtual Blog? Blog { get; set; }
}
