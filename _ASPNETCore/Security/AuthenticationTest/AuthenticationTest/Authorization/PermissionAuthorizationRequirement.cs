using Microsoft.AspNetCore.Authorization;

namespace AuthenticationTest.Authorization;

public class PermissionAuthorizationRequirement : IAuthorizationRequirement
{
    public PermissionAuthorizationRequirement(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
}
