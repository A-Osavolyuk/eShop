namespace eShop.AuthWebApi.Exceptions
{
    public class UserNotInRoleException(string RoleName) : Exception(string.Format("User has no role {0}", RoleName)), IBadRequestException;
}
