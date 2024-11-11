using eShop.ReviewsApi.Commands.Comments;
using FluentValidation;

namespace eShop.ReviewsApi.Validation;

internal class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Request).SetValidator(new Application.Validation.Comments.CreateCommentValidator());
    }
}