using Volo.Abp.Reflection;

namespace ModuleTest.Permissions;

public class ModuleTestPermissions
{
    public const string GroupName = "ModuleTest";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ModuleTestPermissions));
    }
}
