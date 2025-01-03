﻿using eShop.Domain.Entities.CartApi;
using eShop.Domain.Models.Store;

namespace eShop.Domain.Interfaces;

public interface ILocalDataAccessor
{
    public ValueTask<bool> IsFavoritesExistsAsync();
    public ValueTask CreateFavoritesAsync(FavoritesStore favoritesStore);
    public ValueTask<int> GetStoreItemsCountAsync();
    public ValueTask<CartStore> ReadCartAsync();
    public ValueTask CreateCartAsync(CartStore cartStore);
    public ValueTask<bool> IsCartExistsAsync();
    public ValueTask AddToCartAsync(CartItem item);
    public ValueTask<bool> IsInFavoriteGoodsAsync(string id);
    public ValueTask RemoveFromFavoritesAsync(string id);
    public ValueTask<FavoritesStore> ReadFavoritesAsync();
    public ValueTask AddToFavoritesAsync(FavoritesItem item);
    public ValueTask RemoveAvatarLinkAsync();
    public ValueTask WriteAvatarLinkAsync(string link);
    public ValueTask<string> ReadAvatarLinkAsync();
    public ValueTask WriteUserDataAsync(UserStore user);
    public ValueTask<UserStore> ReadUserDataAsync();
    public ValueTask WritePersonalDataAsync(PersonalDataModel personalDataModel);
    public ValueTask<PersonalDataModel> ReadPersonalDataAsync();
    public ValueTask WriteSecurityDataAsync(SecurityData securityData);
    public ValueTask<SecurityData> ReadSecurityDataAsync();
    public ValueTask ClearAsync();
    public ValueTask SetCartAsync(CartStore cartStore);
}