using Microsoft.EntityFrameworkCore;
using RelationshipTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationshipTest;

public class EDbContext : DbContext
{
    public virtual DbSet<Blog> Blogs { get; set; } = default!;
    public virtual DbSet<Post> Posts { get; set; } = default!;

    public virtual IQueryable<DeleteTest> DeleteTest {
        get { return GetQueryable<DeleteTest>(); }
    }

    public EDbContext() { }

    public EDbContext(DbContextOptions<EDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("server=localhost;database=EfCoreRelationship;uid=sa;pwd=Qwe123456;");
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .HasMany(t => t.Posts)
            .WithOne();

        modelBuilder.Entity<DeleteTest>();

        base.OnModelCreating(modelBuilder);
    }

    public virtual IQueryable<T> GetQueryable<T>() where T : class, IDeleteEntity
    {
        return Set<T>().AsQueryable();
    }
}
