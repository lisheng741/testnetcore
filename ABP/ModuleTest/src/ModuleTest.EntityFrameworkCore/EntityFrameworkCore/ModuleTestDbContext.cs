using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ModuleTest.EntityFrameworkCore;

[ConnectionStringName(ModuleTestDbProperties.ConnectionStringName)]
public class ModuleTestDbContext : AbpDbContext<ModuleTestDbContext>, IModuleTestDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public ModuleTestDbContext(DbContextOptions<ModuleTestDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureModuleTest();
    }
}
