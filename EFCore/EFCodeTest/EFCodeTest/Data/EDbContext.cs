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
            builder.Entity<User>();
            builder.Entity<Role>();
            base.OnModelCreating(builder);
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
    }
}
