namespace eShop.AuthApi.Extensions
{
    public static class RoleManagerExtensions
    {
        public static async Task<IEnumerable<RoleInfo>?> GetRolesInfoAsync<TRole>(this RoleManager<TRole> roleManager, IEnumerable<string> roles) where TRole : IdentityRole
        {
            if (roles is null || !roles.Any())
            {
                return null;
            }

            var rolesInfo = new List<RoleInfo>();

            foreach (var role in roles)
            {
                var roleInfo = await roleManager.FindByNameAsync(role);

                rolesInfo.Add(new RoleInfo()
                {
                    Id = Guid.Parse(roleInfo!.Id),
                    Name = roleInfo.Name!,
                    NormalizedName = roleInfo.NormalizedName!
                });
            }

            return rolesInfo;
        }
    }
}
