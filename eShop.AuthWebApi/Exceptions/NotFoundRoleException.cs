namespace eShop.AuthWebApi.Exceptions
{
    public class NotFoundRoleException : Exception, INotFoundException
    {
        public NotFoundRoleException(Guid Id) : base(string.Format("Role with ID {id} not exists or cannot be found.", Id)) { }
        public NotFoundRoleException(string Name) : base(string.Format("Role {name} not exists or cannot be found.", Name)) { }
    }
}
