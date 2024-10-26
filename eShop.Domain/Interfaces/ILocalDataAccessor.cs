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
        public ValueTask WriteUserDataAsync(UserData user);
        public ValueTask<UserData> ReadUserDataAsync();
        public ValueTask WritePersonalDataAsync(PersonalData personalData);
        public ValueTask<PersonalData> ReadPersonalDataAsync();
        public ValueTask WriteSecurityDataAsync(SecurityData securityData);
        public ValueTask<SecurityData> ReadSecurityDataAsync();
        public ValueTask RemoveDataAsync();
    }
}
