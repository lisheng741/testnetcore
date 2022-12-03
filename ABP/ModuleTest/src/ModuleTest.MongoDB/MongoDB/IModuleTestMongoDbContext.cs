using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace ModuleTest.MongoDB;

[ConnectionStringName(ModuleTestDbProperties.ConnectionStringName)]
public interface IModuleTestMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
