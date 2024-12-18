namespace eShop.Domain.Requests.AuthApi.Admin;

public record class DeleteUserAccountRequest
{
    public Guid UserId { get; set; }
}