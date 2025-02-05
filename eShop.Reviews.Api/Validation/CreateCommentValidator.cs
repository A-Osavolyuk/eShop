﻿namespace eShop.Reviews.Api.Validation;

internal sealed class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Request).SetValidator(new Application.Validation.Comments.CreateCommentValidator());
    }
}