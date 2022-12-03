using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace ModuleTest;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class ModuleTestInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ModuleTestInstallerModule>();
        });
    }
}
