using ModuleTest.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ModuleTest;

public abstract class ModuleTestController : AbpControllerBase
{
    protected ModuleTestController()
    {
        LocalizationResource = typeof(ModuleTestResource);
    }
}
