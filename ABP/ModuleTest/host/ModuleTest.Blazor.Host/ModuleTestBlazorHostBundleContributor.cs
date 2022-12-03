using Volo.Abp.Bundling;

namespace ModuleTest.Blazor.Host;

public class ModuleTestBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
