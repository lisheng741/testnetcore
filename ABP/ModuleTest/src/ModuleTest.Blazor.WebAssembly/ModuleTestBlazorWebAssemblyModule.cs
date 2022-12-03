using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace ModuleTest.Blazor.WebAssembly;

[DependsOn(
    typeof(ModuleTestBlazorModule),
    typeof(ModuleTestHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class ModuleTestBlazorWebAssemblyModule : AbpModule
{

}
