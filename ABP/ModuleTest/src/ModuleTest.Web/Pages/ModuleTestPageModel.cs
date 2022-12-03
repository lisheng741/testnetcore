using ModuleTest.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ModuleTest.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ModuleTestPageModel : AbpPageModel
{
    protected ModuleTestPageModel()
    {
        LocalizationResourceType = typeof(ModuleTestResource);
        ObjectMapperContext = typeof(ModuleTestWebModule);
    }
}
