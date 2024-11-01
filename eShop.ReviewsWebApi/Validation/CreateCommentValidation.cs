using eShop.Application.Validation.Comments;
using eShop.ReviewsWebApi.Commands.Comments;
using FluentValidation;

namespace eShop.ReviewsWebApi.Validation;

public class CreateCommentValidation : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidation()
    {
        RuleFor(x => x.Request).SetValidator(new CreateCommentValidator());
    }
}