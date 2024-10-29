using eShop.Domain.Interfaces;

namespace eShop.CartWebApi.Exceptions;

public class NotFoundFavoritesException(Guid favoritesId) : Exception(
    string.Format("Cannot find favorites with ID {favoritesId}.", favoritesId)), INotFoundException;