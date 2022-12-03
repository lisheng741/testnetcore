using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace ModuleTest;

[DependsOn(
    typeof(ModuleTestDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class ModuleTestApplicationContractsModule : AbpModule
{

}
