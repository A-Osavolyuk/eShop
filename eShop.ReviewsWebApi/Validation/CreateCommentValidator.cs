using eShop.Application.Validation.Comments;
using eShop.ReviewsWebApi.Commands.Comments;
using FluentValidation;

namespace eShop.ReviewsWebApi.Validation;

public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Request).SetValidator(new Application.Validation.Comments.CreateCommentValidator());
    }
}