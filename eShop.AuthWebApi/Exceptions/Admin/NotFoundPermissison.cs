namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotFoundPermissison(string Name) : Exception(string.Format("Cannot find permission {0}", Name)), INotFoundException;
}
