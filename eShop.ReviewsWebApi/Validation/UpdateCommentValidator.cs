using eShop.ReviewsWebApi.Commands.Comments;
using FluentValidation;

namespace eShop.ReviewsWebApi.Validation;

public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentValidator()
    {
        RuleFor(x => x.Request).SetValidator(new Application.Validation.Comments.UpdateCommentValidator());
    }
}