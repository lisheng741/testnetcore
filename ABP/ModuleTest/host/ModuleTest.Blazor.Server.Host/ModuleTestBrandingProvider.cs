using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ModuleTest.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class ModuleTestBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ModuleTest";
}
