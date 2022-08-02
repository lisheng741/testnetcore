using EFCoreConcurrentTest;
using Microsoft.EntityFrameworkCore;


public class EDbContext : DbContext
{
    public virtual DbSet<Blog> Blog { get; set; }
    public virtual DbSet<TestConcurrent> TestConcurrent { get; set; }

    public virtual DbSet<TestRowVersion> TestRowVersion { get; set; }

    public EDbContext(DbContextOptions<EDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        var changes = ChangeTracker.Entries<TestConcurrent>();
        foreach (var change in changes)
        {
            change.Entity.ConcurrentStamp += "A";
        }

        return base.SaveChanges();
    }
}
