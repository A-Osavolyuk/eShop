namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class UserNotInRoleException(string RoleName) : Exception(string.Format("User has no role {0}", RoleName)), IBadRequestException;
}
