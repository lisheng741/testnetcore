using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace ModuleTest.Web.Menus;

public class ModuleTestMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(ModuleTestMenus.Prefix, displayName: "ModuleTest", "~/ModuleTest", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
