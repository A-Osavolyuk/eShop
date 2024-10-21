using eShop.Domain.Models;

namespace eShop.Domain.Interfaces
{
    public interface ILocalDataAccessor
    {
        public ValueTask<bool> IsFavoritesExistsAsync();
        public ValueTask CreateFavoritesAsync(Favorites favorites);
        public ValueTask<int> GetStoreItemsCountAsync();
        public ValueTask<Cart> ReadCartAsync();
        public ValueTask CreateCartAsync(Cart cart);
        public ValueTask<bool> IsCartExistsAsync();
        public ValueTask AddToCartAsync(StoreItem item);
        public ValueTask<bool> IsInFavoriteGoodsAsync(string id);
        public ValueTask RemoveFromFavoritesAsync(string id);
        public ValueTask<Favorites> ReadFavoriteGoodsAsync();
        public ValueTask AddoFavoritesAsync(StoreItem item);
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
