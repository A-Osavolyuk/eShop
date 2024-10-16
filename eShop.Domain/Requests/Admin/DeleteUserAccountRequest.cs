using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Admin
{
    public record class DeleteUserAccountRequest : RequestBase
    {
        public Guid UserId { get; set; }
    }
}
