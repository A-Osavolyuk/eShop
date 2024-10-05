using Microsoft.AspNetCore.Authorization;

namespace eShop.AuthWebApi.Security.Authorization
{
    public class PermissionRequirement(string permissionName) : IAuthorizationRequirement
    {
        public string PermissionName { get; } = permissionName;
    }

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "Permissison" && c.Value == requirement.PermissionName)) 
            { 
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
