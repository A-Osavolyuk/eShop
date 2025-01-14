using eShop.Reviews.Api.Commands.Comments;

namespace eShop.Reviews.Api.Validation;

internal sealed class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentValidator()
    {
        RuleFor(x => x.Request).SetValidator(new Application.Validation.Comments.UpdateCommentValidator());
    }
}