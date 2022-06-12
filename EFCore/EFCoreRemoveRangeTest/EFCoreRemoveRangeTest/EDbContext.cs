using Microsoft.EntityFrameworkCore;

namespace EFCoreRemoveRangeTest;

public class EDbContext : DbContext
{
    public virtual DbSet<DeleteTest> DeleteTest { get; set; } = default!;

    public EDbContext(DbContextOptions<EDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {

    }
}
