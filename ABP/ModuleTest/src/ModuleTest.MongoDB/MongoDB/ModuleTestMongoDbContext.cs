using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace ModuleTest.MongoDB;

[ConnectionStringName(ModuleTestDbProperties.ConnectionStringName)]
public class ModuleTestMongoDbContext : AbpMongoDbContext, IModuleTestMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureModuleTest();
    }
}
