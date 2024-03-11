namespace eShop.Domain.Exceptions.Auth
{
    public class InvalidRegisterAttemptException : Exception
    {
        public InvalidRegisterAttemptException(string errorType, IEnumerable<string> Errors) : base(errorType)
        {
            ErrorType = errorType;
            this.Errors = Errors;
        }

        public string ErrorType { get; }
        public IEnumerable<string> Errors { get; }
    }
}
