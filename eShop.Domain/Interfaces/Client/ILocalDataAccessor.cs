using eShop.Domain.Types;

namespace eShop.Domain.Interfaces.Client;

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
    public ValueTask WriteSecurityDataAsync(SecurityData securityData);
    public ValueTask<SecurityData> ReadSecurityDataAsync();
    public ValueTask ClearAsync();
    public ValueTask SetCartAsync(CartModel cartModel);
}