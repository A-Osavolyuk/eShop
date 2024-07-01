using eShop.Domain.Exceptions;
using eShop.ReviewsWebApi.Repositories;
using FluentValidation;

namespace eShop.ReviewsWebApi.Commands.Reviews
{
    public record UpdateReviewCommand(UpdateReviewRequest UpdateReviewRequest) : IRequest<Result<Unit>>;

    public class UpdateReviewCommandHandler(IReviewRepository repository, IValidator<UpdateReviewRequest> validator) : IRequestHandler<UpdateReviewCommand, Result<Unit>>
    {
        private readonly IReviewRepository repository = repository;
        private readonly IValidator<UpdateReviewRequest> validator = validator;

        public async Task<Result<Unit>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.UpdateReviewRequest, cancellationToken);

            if (validationResult.IsValid) 
            { 
                var result = await repository.UpdateReviewAsync(request.UpdateReviewRequest);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
