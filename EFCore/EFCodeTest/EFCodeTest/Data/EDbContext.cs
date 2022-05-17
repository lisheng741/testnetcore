using EFCodeTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCodeTest.Data
{
    public class EDbContext : DbContext
    {
        public EDbContext(DbContextOptions<EDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<User>();
            builder.Entity<UserDetail>()
                .HasOne(p => p.User)
                .WithOne(b => b.UserDetail)
                .IsRequired(false);
            builder.Entity<UserRole>()
                .HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<UserRole>()
                .HasOne(p => p.User)
                .WithMany(b => b.Roles)
                .IsRequired(false);

            builder.Entity<Post>()
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .IsRequired(false);

            builder.Entity<Order>()
                .HasMany(p => p.Lines)
                .WithOne();

            base.OnModelCreating(builder);
        }

        public IQueryable<Blog> BlogQueryable
        {
            get
            {
                return Blog.Where(t => t.BlogId == 1);
            }
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Blog> Blog { get; set; }
        public virtual DbSet<Order> Order { get; set; }
    }
}
