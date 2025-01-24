using eShop.Domain.DTOs.Api.Auth;
using eShop.Domain.Types;

namespace eShop.Auth.Api.Extensions;

public static class RoleManagerExtensions
{
    public static async Task<IEnumerable<RoleData>?> GetRolesInfoAsync<TRole>(this RoleManager<TRole> roleManager, IEnumerable<string> roles) where TRole : IdentityRole
    {
        var rolesList = roles.ToList();
        if (!rolesList.Any())
        {
            return null;
        }

        var rolesInfo = new List<RoleData>();

        foreach (var role in rolesList)
        {
            var roleInfo = await roleManager.FindByNameAsync(role);

            rolesInfo.Add(new RoleData()
            {
                Id = Guid.Parse(roleInfo!.Id),
                Name = roleInfo.Name!,
                NormalizedName = roleInfo.NormalizedName!
            });
        }

        return rolesInfo;
    }
}