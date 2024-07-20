using eShop.Domain.DTOs.Requests.Cart;
using eShop.Domain.DTOs;

namespace eShop.Domain.Interfaces
{
    public interface ICartService
    {
        public ValueTask<ResponseDTO> AddGoodAsync(AddGoodToCartRequest request);
        public ValueTask<ResponseDTO> CreateCartAsync(CreateCartRequest request);
        public ValueTask<ResponseDTO> GetCartByUserIdAsync(Guid UserId);
        public ValueTask<ResponseDTO> UpdateCartAsync(UpdateCartRequest request);
    }
}
