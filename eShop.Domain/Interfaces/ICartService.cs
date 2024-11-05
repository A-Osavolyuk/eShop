using eShop.Domain.DTOs;
using eShop.Domain.Requests.Cart;

namespace eShop.Domain.Interfaces
{
    public interface ICartService
    {
        public ValueTask<ResponseDTO> GetCartAsync(Guid userId);
        public ValueTask<ResponseDTO> UpdateCartAsync(UpdateCartRequest request);
    }
}
