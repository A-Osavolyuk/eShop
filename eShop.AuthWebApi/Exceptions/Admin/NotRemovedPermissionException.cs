namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotRemovedPermissionException(IEnumerable<IdentityError> errors) 
        : Exception(string.Format("Failed on removing user from permissions with error message: {0}", errors.First().Description)), IInternalServerError;
}
