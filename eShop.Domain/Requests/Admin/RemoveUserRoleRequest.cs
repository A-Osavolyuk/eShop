﻿using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;

namespace eShop.Domain.Requests.Admin
{
    public record RemoveUserRoleRequest : RequestBase
    {
        public Guid UserId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
