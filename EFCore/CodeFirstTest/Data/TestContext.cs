using Microsoft.EntityFrameworkCore;

namespace CodeFirstTest;

public class TestContext : DbContext
{
    public TestContext(DbContextOptions<TestContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            // options.UseMySql("server=localhost;port=3306;database=efcore;user=root;password=123456;charset=utf8mb4;", ServerVersion.Parse("8.0.26"));
            // options.UseSqlServer("server=localhost;database=efcore;uid=sa;pwd=Qwe123456;");
        }
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>();

        base.OnModelCreating(builder);
    }

    public virtual DbSet<User> User { get; set; }
}