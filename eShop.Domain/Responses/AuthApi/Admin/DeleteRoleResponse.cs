namespace eShop.Domain.Responses.AuthApi.Admin;

public class DeleteRoleResponse
{
    public string Message { get; set; } = string.Empty;
    public bool Succeeded { get; set; }
}