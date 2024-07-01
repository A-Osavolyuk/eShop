using eShop.Domain.DTOs.Requests.Review;
using FluentValidation;

namespace eShop.Application.Validation.Reviews
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewRequest>
    {
        public CreateReviewValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name cannot be empty");

            RuleFor(x => x.CreatedAt).Must(x => x <= DateTime.UtcNow);

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 5).WithMessage("Rating must be in range from 0 to 5");

            RuleFor(x => x.Text)
                .MinimumLength(8).When(x => !string.IsNullOrEmpty(x.Text)).WithMessage("Review text must contain at least 8 characters")
                .MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Text)).WithMessage("Review text cannot be longer then 2000 characters");

            RuleFor(x => x.UserId).IsValidGuid();
            RuleFor(x => x.ReviewId).IsValidGuid();
            RuleFor(x => x.ProductId).IsValidGuid();
        }
    }

    public class UpdateReviewValidator : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name cannot be empty");

            RuleFor(x => x.UpdatedAt).Must(x => x <= DateTime.UtcNow);

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 5).WithMessage("Rating must be in range from 0 to 5");

            RuleFor(x => x.Text)
                .MinimumLength(8).When(x => !string.IsNullOrEmpty(x.Text)).WithMessage("Review text must contain at least 8 characters")
                .MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Text)).WithMessage("Review text cannot be longer then 2000 characters");

            RuleFor(x => x.UserId).IsValidGuid();
            RuleFor(x => x.ReviewId).IsValidGuid();
            RuleFor(x => x.ProductId).IsValidGuid();
        }
    }
}
