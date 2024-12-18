using eShop.Domain.Entities.CartApi;
using UserData = eShop.Domain.Models.UserData;

namespace eShop.Domain.Interfaces;

public interface ILocalDataAccessor
{
    public ValueTask<bool> IsFavoritesExistsAsync();
    public ValueTask CreateFavoritesAsync(FavoritesModel favoritesModel);
    public ValueTask<int> GetStoreItemsCountAsync();
    public ValueTask<CartModel> ReadCartAsync();
    public ValueTask CreateCartAsync(CartModel cartModel);
    public ValueTask<bool> IsCartExistsAsync();
    public ValueTask AddToCartAsync(CartItem item);
    public ValueTask<bool> IsInFavoriteGoodsAsync(string id);
    public ValueTask RemoveFromFavoritesAsync(string id);
    public ValueTask<FavoritesModel> ReadFavoritesAsync();
    public ValueTask AddToFavoritesAsync(FavoritesItem item);
    public ValueTask RemoveAvatarLinkAsync();
    public ValueTask WriteAvatarLinkAsync(string link);
    public ValueTask<string> ReadAvatarLinkAsync();
    public ValueTask WriteUserDataAsync(UserData user);
    public ValueTask<UserData> ReadUserDataAsync();
    public ValueTask WritePersonalDataAsync(PersonalDataModel personalDataModel);
    public ValueTask<PersonalDataModel> ReadPersonalDataAsync();
    public ValueTask WriteSecurityDataAsync(SecurityData securityData);
    public ValueTask<SecurityData> ReadSecurityDataAsync();
    public ValueTask RemoveDataAsync();
    public ValueTask SetCartAsync(CartModel cartModel);
}