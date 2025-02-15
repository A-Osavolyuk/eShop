﻿namespace eShop.Auth.Api.Entities;

public class AppUser : IdentityUser
{
    public PersonalDataEntity? PersonalData { get; set; }
    public SecurityTokenEntity? AuthenticationToken { get; set; }
    public ICollection<UserPermissionsEntity> Permissions { get; set; } = null!;
}