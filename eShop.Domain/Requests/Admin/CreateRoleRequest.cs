namespace eShop.Domain.Requests.Admin
{
    public record CreateRoleRequest : RequestBase
    {
        public string Name { get; set; } = string.Empty;
    }
}
