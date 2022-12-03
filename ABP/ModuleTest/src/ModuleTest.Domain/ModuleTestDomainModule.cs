using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace ModuleTest;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ModuleTestDomainSharedModule)
)]
public class ModuleTestDomainModule : AbpModule
{

}
