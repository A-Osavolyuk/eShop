using eShop.Domain.Requests.Admin;
using FluentValidation;

namespace eShop.Application.Validation.Admin
{
    public class CreateRoleValidator : AbstractValidator<CreateRoleRequest>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
                .MaximumLength(32).WithMessage("Name cannot be longer then 32 characters long");
        }
    }
}
