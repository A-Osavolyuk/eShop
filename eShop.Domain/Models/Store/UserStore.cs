namespace eShop.Domain.Models.Store;

public class UserStore
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    public List<string> Roles { get; set; } = null!;
    public List<string> Permissions { get; set; } = null!;
}