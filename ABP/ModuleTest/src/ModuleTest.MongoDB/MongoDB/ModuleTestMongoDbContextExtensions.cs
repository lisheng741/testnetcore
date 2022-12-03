using Volo.Abp;
using Volo.Abp.MongoDB;

namespace ModuleTest.MongoDB;

public static class ModuleTestMongoDbContextExtensions
{
    public static void ConfigureModuleTest(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
