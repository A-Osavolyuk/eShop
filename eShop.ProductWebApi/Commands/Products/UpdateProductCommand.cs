using eShop.Domain.DTOs.Requests;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Products
{
    public record UpdateProductCommand<TRequest, TResponse>(TRequest ProductRequestBase, Guid Id) : IRequest<Result<TResponse>> 
        where TRequest : UpdateProductRequestBase 
        where TResponse : ProductDTO;

    public class UpdateProductCommandHandler<TRequest, TResponse, TEntity>(
        IProductRepository repository, 
        IValidator<TRequest> validator) 
        : IRequestHandler<UpdateProductCommand<TRequest, TResponse>, Result<TResponse>>
        where TRequest : UpdateProductRequestBase
        where TResponse:ProductDTO
        where TEntity : Product
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<TRequest> validator = validator;

        public async Task<Result<TResponse>> Handle(UpdateProductCommand<TRequest, TResponse> request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.ProductRequestBase, cancellationToken);

            if (validationResult.IsValid) 
            {
                request.ProductRequestBase.Id = request.Id;
                var result = await repository.UpdateProductAsync<TResponse, TEntity, TRequest>(request.ProductRequestBase);
                return result;
            }

            return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
