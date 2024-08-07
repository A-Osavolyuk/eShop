using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests.Cart;

namespace eShop.Domain.Interfaces
{
    public interface ICartService
    {
        public ValueTask<ResponseDTO> GetCartByUserIdAsync(Guid Id);
        public ValueTask<ResponseDTO> UpdateCartAsync(UpdateCartRequest updateCartRequest);
    }
}
