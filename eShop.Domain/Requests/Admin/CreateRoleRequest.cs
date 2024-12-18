namespace eShop.Domain.Requests.Admin
{
    public record CreateRoleRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
