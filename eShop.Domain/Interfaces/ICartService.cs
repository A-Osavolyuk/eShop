using eShop.Domain.Common.Api;
using eShop.Domain.Requests.CartApi.Cart;

namespace eShop.Domain.Interfaces;

public interface ICartService
{
    public ValueTask<Response> GetCartAsync(Guid userId);
    public ValueTask<Response> UpdateCartAsync(UpdateCartRequest request);
}