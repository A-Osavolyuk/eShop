using Microsoft.AspNetCore.Authorization;

namespace eShop.AuthApi.Security.Authorization
{
    public class PermissionRequirement(string permissionName) : IAuthorizationRequirement
    {
        public string PermissionName { get; } = permissionName;
    }

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == CustomClaimTypes.Permission && c.Value == requirement.PermissionName) || context.User.IsInRole("Admin")) 
            { 
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
