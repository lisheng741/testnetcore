using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace ModuleTest.EntityFrameworkCore;

[ConnectionStringName(ModuleTestDbProperties.ConnectionStringName)]
public interface IModuleTestDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
