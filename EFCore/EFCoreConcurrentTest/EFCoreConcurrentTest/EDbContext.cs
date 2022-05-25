using Microsoft.EntityFrameworkCore;


public class EDbContext : DbContext
{
    public virtual DbSet<Blog> Blog { get; set; }

    public EDbContext(DbContextOptions<EDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
