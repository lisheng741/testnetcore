using ModuleTest.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ModuleTest.Permissions;

public class ModuleTestPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ModuleTestPermissions.GroupName, L("Permission:ModuleTest"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ModuleTestResource>(name);
    }
}
