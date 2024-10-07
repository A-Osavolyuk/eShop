using eShop.Domain.DTOs;
using eShop.Domain.Models;

namespace eShop.Domain.Interfaces
{
    public interface ILocalDataAccessor
    {
        public ValueTask WriteUserDataAsync(UserModel user);
        public ValueTask<UserModel> ReadUserDataAsync();
        public ValueTask RemoveDataAsync();
        public ValueTask SetCartAsync(CartDTO Cart);
        public ValueTask<CartDTO> GetCartAsync();
    }
}
