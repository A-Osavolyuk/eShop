namespace eShop.CartWebApi.Commands.Cart
{
    public record CreateCartCommand(CreateCartRequest CreateCartRequest) : IRequest<Result<Unit>>;

    public class CreateCartCommandHandler(
        ICartRepository repository,
        IValidator<CreateCartRequest> validator,
        IRequestClient<UserExistsRequest> requestClient) : IRequestHandler<CreateCartCommand, Result<Unit>>
    {
        private readonly ICartRepository repository = repository;
        private readonly IValidator<CreateCartRequest> validator = validator;
        private readonly IRequestClient<UserExistsRequest> requestClient = requestClient;

        public async Task<Result<Unit>> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CreateCartRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var handler = requestClient.Create(request.CreateCartRequest);
                var userExists = await handler.GetResponse<UserExistsResponse>();

                if (userExists.Message.Exists)
                {
                    var result = await repository.CreateCartAsync(request.CreateCartRequest);
                    return result;
                }

                return new(new NotFoundUserException(request.CreateCartRequest.UserId));
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
