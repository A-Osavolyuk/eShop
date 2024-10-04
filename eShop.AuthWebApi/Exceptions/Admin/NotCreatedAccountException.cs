namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotCreatedAccountException(IEnumerable<IdentityError> errors) 
        : Exception(string.Format("Failed to create user  account with error message: {message}", errors.First().Description)), IInternalServerError;
}
