using Microsoft.AspNetCore.Authorization;

namespace AuthenticationTest.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
    {
        var permissions = context.User.Claims.Where(_ => _.Type == "Permission").Select(_ => _.Value).ToList();
        if (permissions.Any(_ => _.StartsWith(requirement.Name)))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}
