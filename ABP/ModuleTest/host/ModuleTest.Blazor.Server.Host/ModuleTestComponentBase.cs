using ModuleTest.Localization;
using Volo.Abp.AspNetCore.Components;

namespace ModuleTest.Blazor.Server.Host;

public abstract class ModuleTestComponentBase : AbpComponentBase
{
    protected ModuleTestComponentBase()
    {
        LocalizationResource = typeof(ModuleTestResource);
    }
}
