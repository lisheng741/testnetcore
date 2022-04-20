using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace EFCoreTestApi;

public partial class MySqlDbContext : DbContext
{
    public MySqlDbContext() { }
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
    public DbSet<UserDetail> UserDetail { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseMySql("server=localhost;port=3306;database=efcore;user=root;password=123456;charset=utf8mb4;", ServerVersion.Parse("8.0.26"));
            //options.UseSqlServer("server=localhost;database=efcore;uid=sa;pwd=Qwe123456;");
        }
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasOne( _ => _.UserDetail);

        builder.Entity<UserDetail>();

        base.OnModelCreating(builder);
    }
}

