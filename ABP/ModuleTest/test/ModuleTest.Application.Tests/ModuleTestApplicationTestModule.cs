using Volo.Abp.Modularity;

namespace ModuleTest;

[DependsOn(
    typeof(ModuleTestApplicationModule),
    typeof(ModuleTestDomainTestModule)
    )]
public class ModuleTestApplicationTestModule : AbpModule
{

}
