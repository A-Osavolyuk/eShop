using Microsoft.AspNetCore.Identity;

namespace eShop.Domain.Exceptions
{
    public class IdentityException(string errorType = "Identity Error", IEnumerable<IdentityError> errors = null!) : Exception(errorType);
}
