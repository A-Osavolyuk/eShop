using eShop.Domain.Models;

namespace eShop.Domain.Interfaces
{
    public interface ILocalDataAccessor
    {
        public ValueTask<Cart> ReadCartAsync();
        public ValueTask CreateCartAsync(Cart cart);
        public ValueTask<bool> IsCartExistsAsync();
        public ValueTask AddToCartAsync(CartItem item);
        public ValueTask<bool> IsInFavoriteGoodsAsync(string id);
        public ValueTask RemoveFavoriteGoodAsync(string id);
        public ValueTask<IEnumerable<FavoriteGoodModel>> ReadFavoriteGoodsAsync();
        public ValueTask AddFavoriteGoodAsync(FavoriteGoodModel model);
        public ValueTask RemoveAvatarLinkAsync();
        public ValueTask WriteAvatarLinkAsync(string link);
        public ValueTask<string> ReadAvatarLinkAsync();
        public ValueTask WriteUserDataAsync(UserDataModel user);
        public ValueTask<UserDataModel> ReadUserDataAsync();
        public ValueTask WritePersonalDataAsync(PersonalDataModel personalData);
        public ValueTask<PersonalDataModel> ReadPersonalDataAsync();
        public ValueTask WriteSecurityDataAsync(SecurityDataModel securityData);
        public ValueTask<SecurityDataModel> ReadSecurityDataAsync();
        public ValueTask RemoveDataAsync();
    }
}
