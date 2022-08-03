using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreMySqlConcurrencyTest.Models;

public class Blog : EntityBase<Guid>
{
    /// <summary>
    /// 博客名
    /// </summary>
    public string? Name { get; set; }

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
