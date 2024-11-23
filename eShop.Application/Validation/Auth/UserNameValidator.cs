﻿namespace eShop.Application.Validation.Auth
{
    public class UserNameValidator : AbstractValidator<ChangeUserNameRequest>
    {
        public UserNameValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name cannot be empty.")
                .MinimumLength(3).WithMessage("User name must be at least 3 characters long")
                .MaximumLength(18).WithMessage("User name cannot be longer then 18 characters.");
        }
    }
}
