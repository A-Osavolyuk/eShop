﻿using eShop.Domain.Requests.AuthApi.Auth;

namespace eShop.Application.Validation.Auth;

public class TwoFactorLoginValidator : AbstractValidator<TwoFactorAuthenticationLoginRequest>
{
    public TwoFactorLoginValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code cannot be empty.")
            .Length(6).WithMessage("Must contain 6 characters.");
    }
}