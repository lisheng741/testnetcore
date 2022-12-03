using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace ModuleTest;

[DependsOn(
    typeof(ModuleTestApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class ModuleTestHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(ModuleTestApplicationContractsModule).Assembly,
            ModuleTestRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ModuleTestHttpApiClientModule>();
        });

    }
}
