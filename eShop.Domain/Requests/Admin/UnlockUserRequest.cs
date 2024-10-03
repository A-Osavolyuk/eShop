using eShop.Domain.DTOs.Requests;

namespace eShop.Domain.Requests.Admin
{
    public record UnlockUserRequest : RequestBase
    {
        public Guid UserId { get; set; }
    }
}
