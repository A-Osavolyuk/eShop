namespace eShop.AuthWebApi.Exceptions.Admin
{
    public class NotFoundRoleException : Exception, INotFoundException
    {
        public NotFoundRoleException(Guid Id) : base(string.Format("Role with ID {0} not exists or cannot be found.", Id)) { }
        public NotFoundRoleException(string Name) : base(string.Format("Role {0} not exists or cannot be found.", Name)) { }
    }
}
