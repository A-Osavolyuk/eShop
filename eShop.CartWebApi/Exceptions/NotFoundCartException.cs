using eShop.Domain.Interfaces;

namespace eShop.CartWebApi.Exceptions
{
    public class NotFoundCartException(Guid Id) : Exception(string.Format("Cannot find cart with id: {0}", Id)), INotFoundException;
}
