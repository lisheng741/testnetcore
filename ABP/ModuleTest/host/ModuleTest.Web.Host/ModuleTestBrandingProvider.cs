using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ModuleTest;

[Dependency(ReplaceServices = true)]
public class ModuleTestBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ModuleTest";
}
