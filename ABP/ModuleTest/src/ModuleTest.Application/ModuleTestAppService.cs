using ModuleTest.Localization;
using Volo.Abp.Application.Services;

namespace ModuleTest;

public abstract class ModuleTestAppService : ApplicationService
{
    protected ModuleTestAppService()
    {
        LocalizationResource = typeof(ModuleTestResource);
        ObjectMapperContext = typeof(ModuleTestApplicationModule);
    }
}
