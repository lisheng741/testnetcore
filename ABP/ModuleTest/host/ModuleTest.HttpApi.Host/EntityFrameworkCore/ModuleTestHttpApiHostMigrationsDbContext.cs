using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ModuleTest.EntityFrameworkCore;

public class ModuleTestHttpApiHostMigrationsDbContext : AbpDbContext<ModuleTestHttpApiHostMigrationsDbContext>
{
    public ModuleTestHttpApiHostMigrationsDbContext(DbContextOptions<ModuleTestHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureModuleTest();
    }
}
