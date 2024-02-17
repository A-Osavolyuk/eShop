namespace eShop.Domain.Exceptions
{
    public class FailedValidationException : Exception
    {
        public FailedValidationException(string ErrorMessage, IEnumerable<string> Errors) : base(ErrorMessage)
        {
            this.Errors = Errors;
        }

        public IEnumerable<string> Errors { get; }
    };
}
