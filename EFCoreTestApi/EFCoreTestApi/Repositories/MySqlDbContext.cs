using Microsoft.EntityFrameworkCore;

namespace EFCoreTestApi;

public partial class MySqlDbContext : DbContext
{
    public MySqlDbContext() { }
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) { }

    public DbSet<User>? User { get; set; }
    public DbSet<UserDetail>? UserDetail { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseMySql("server=127.0.0.1;port=3306;database=efcore;user=root;password=123456;charset=utf8;", ServerVersion.Parse("5.7.19"));
        }
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .ToTable("user")
            .HasOne( _ => _.UserDetail);

        builder.Entity<UserDetail>()
            .ToTable("userDetail");

        base.OnModelCreating(builder);
    }
}

