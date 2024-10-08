﻿namespace eShop.Domain.DTOs.Requests.Admin
{
    public record class AssignRoleRequest : RequestBase
    {
        public Guid UserId { get; set; } 
        public string RoleName { get; set; } = string.Empty;
    }
}
