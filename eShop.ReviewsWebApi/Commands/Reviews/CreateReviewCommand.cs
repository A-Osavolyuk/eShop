using eShop.Domain.Exceptions;
using eShop.ReviewsWebApi.Repositories;
using FluentValidation;

namespace eShop.ReviewsWebApi.Commands.Reviews
{
    public record CreateReviewCommand(CreateReviewRequest Request) : IRequest<Result<Unit>>;

    public class CreateReviewCommandHandler(IReviewRepository repository, IValidator<CreateReviewRequest> validator)  : IRequestHandler<CreateReviewCommand, Result<Unit>>
    {
        private readonly IReviewRepository repository = repository;
        private readonly IValidator<CreateReviewRequest> validator = validator;

        public async Task<Result<Unit>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await repository.CreateReviewAsync(request.Request);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}

