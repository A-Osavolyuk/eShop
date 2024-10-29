using eShop.Domain.Interfaces;

namespace eShop.CartWebApi.Exceptions;

public class NotFoundFavoritesByUserIdException(Guid Id) : 
    Exception(string.Format("Cannot find favorites with user ID {id}", Id)), INotFoundException;