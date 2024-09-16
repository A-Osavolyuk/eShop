using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Admin
{
    public record DeleteRoleRequest : RequestBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
