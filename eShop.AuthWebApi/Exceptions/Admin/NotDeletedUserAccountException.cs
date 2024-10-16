namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotDeletedUserAccountException(IEnumerable<IdentityError> errors) 
        : Exception(string.Format("Failed on deleting user account with error message: {0}", errors.First().Description)), IInternalServerError;
}
