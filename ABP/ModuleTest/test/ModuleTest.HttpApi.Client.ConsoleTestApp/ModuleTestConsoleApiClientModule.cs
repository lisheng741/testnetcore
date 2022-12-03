using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace ModuleTest;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ModuleTestHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class ModuleTestConsoleApiClientModule : AbpModule
{

}
