using FluentValidation;

namespace eShop.Application.Validation
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, Guid> IsValidGuid<T>(this IRuleBuilder<T, Guid> ruleBuilder)
        {
            return ruleBuilder.Must(guid => guid.ToString().Length == 36)
                .WithMessage("Guid must contain 36 characters. Invalid Guid Format.");
        }
    }
}
