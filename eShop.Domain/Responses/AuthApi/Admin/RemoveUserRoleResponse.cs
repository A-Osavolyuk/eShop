﻿namespace eShop.Domain.Responses.AuthApi.Admin;

public class RemoveUserRoleResponse
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
}