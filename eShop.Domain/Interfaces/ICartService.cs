using eShop.Domain.Common.Api;

namespace eShop.Domain.Interfaces
{
    public interface ICartService
    {
        public ValueTask<Response> GetCartAsync(Guid userId);
        public ValueTask<Response> UpdateCartAsync(UpdateCartRequest request);
    }
}
