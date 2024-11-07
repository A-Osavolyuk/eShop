using eShop.ReviewsApi.Commands.Comments;
using FluentValidation;

namespace eShop.ReviewsApi.Validation;

public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentValidator()
    {
        RuleFor(x => x.Request).SetValidator(new Application.Validation.Comments.UpdateCommentValidator());
    }
}