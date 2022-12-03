using ModuleTest.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ModuleTest;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(ModuleTestEntityFrameworkCoreTestModule)
    )]
public class ModuleTestDomainTestModule : AbpModule
{

}
