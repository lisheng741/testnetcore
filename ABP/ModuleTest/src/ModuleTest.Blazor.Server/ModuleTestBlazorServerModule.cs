using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace ModuleTest.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(ModuleTestBlazorModule)
    )]
public class ModuleTestBlazorServerModule : AbpModule
{

}
