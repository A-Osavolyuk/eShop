using eShop.Domain.DTOs;
using eShop.Domain.Requests.Cart;

namespace eShop.Domain.Interfaces
{
    public interface ICartService
    {
        public ValueTask<ResponseDto> GetCartAsync(Guid userId);
        public ValueTask<ResponseDto> UpdateCartAsync(UpdateCartRequest request);
    }
}
