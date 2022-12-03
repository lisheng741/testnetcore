using ModuleTest.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ModuleTest.Pages;

public abstract class ModuleTestPageModel : AbpPageModel
{
    protected ModuleTestPageModel()
    {
        LocalizationResourceType = typeof(ModuleTestResource);
    }
}
