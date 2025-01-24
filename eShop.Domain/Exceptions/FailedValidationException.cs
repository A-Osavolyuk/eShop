using eShop.Domain.Exceptions.Interfaces;

namespace eShop.Domain.Exceptions;

public class FailedValidationException : Exception, IFailedValidationException
{
    public FailedValidationException(IEnumerable<ValidationFailure> Errors, string ErrorMessage = "Validation error(s)") : base(ErrorMessage)
    {
        this.Errors = Errors.Select(x => x.ErrorMessage);
    }

    public IEnumerable<string> Errors { get; }
};