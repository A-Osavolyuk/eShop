namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotIssuedPermissionsException(IEnumerable<IdentityError> errors) : 
        Exception(string.Format("Failed on issuing permissions with error message: {0}", errors)), IInternalServerError;
}
