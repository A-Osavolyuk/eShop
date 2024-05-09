using eShop.Domain.DTOs.Requests;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Products
{
    public record CreateProductCommand<TRequest, TResponse>(TRequest ProductRequestBase) : IRequest<Result<IEnumerable<TResponse>>> 
        where TRequest : CreateProductRequestBase 
        where TResponse : ProductDTO;

    public class CreateProductCommandHandler<TRequest, TResponse, TEntity>(
        IProductRepository repository, 
        IValidator<TRequest> validator) 
        : IRequestHandler<CreateProductCommand<TRequest, TResponse>, Result<IEnumerable<TResponse>>>
        where TRequest : CreateProductRequestBase
        where TResponse:ProductDTO
        where TEntity : Product
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<TRequest> validator = validator;

        public async Task<Result<IEnumerable<TResponse>>> Handle(CreateProductCommand<TRequest, TResponse> request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.ProductRequestBase, cancellationToken);

            if (validationResult.IsValid) 
            {
                var result = await repository.CreateProductsAsync<TResponse, TEntity, TRequest>(request.ProductRequestBase);
                return result;
            }

            return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
