namespace eShop.AuthWebApi.Exceptions
{
    public class NotFoundUserByIdException : Exception, INotFoundException
    {
        public NotFoundUserByIdException(string Id) : base(string.Format("Cannot find user with ID {0}", Id)) { }
        public NotFoundUserByIdException(Guid Id) : base(string.Format("Cannot find user with ID {0}", Id)) { }
    }
}
