using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ModuleTest.EntityFrameworkCore;

public class ModuleTestHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<ModuleTestHttpApiHostMigrationsDbContext>
{
    public ModuleTestHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<ModuleTestHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("ModuleTest"));

        return new ModuleTestHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
