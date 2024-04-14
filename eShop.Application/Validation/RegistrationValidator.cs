﻿using eShop.Domain.DTOs.Requests.Auth;
using FluentValidation;

namespace eShop.Application.Validation
{
    public class RegistrationValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationValidator()
        {
            //RuleFor(p => p.Name)
            //    .NotEmpty().WithMessage("Name is must.")
            //    .MinimumLength(2).WithMessage("Name must contains at least 2 characters.")
            //    .MaximumLength(18).WithMessage("Name cannot be longer then 18 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is must.")
                .EmailAddress().WithMessage("Invalid format of email address.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is must.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(32).WithMessage("Password cannot be longer then 32 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one numeric digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("You must confirm your password.")
                .Equal(x => x.Password).WithMessage("Must be the same with password.");

        }
    }
}
