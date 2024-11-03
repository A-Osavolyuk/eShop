using eShop.Domain.DTOs;
using eShop.Domain.Models;
using eShop.Domain.Requests.Cart;

namespace eShop.Domain.Interfaces
{
    public interface ICartService
    {
        public ValueTask<ResponseDTO> GetCartAsync(GetCartRequest request);
        public ValueTask<ResponseDTO> UpdateCartAsync(UpdateCartRequest request);
    }
}
