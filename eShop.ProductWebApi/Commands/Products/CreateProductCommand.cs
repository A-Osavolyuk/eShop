using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Products
{
    public record CreateProductCommand<TResponse, TRequest>(IEnumerable<CreateProductRequest> Request) : IRequest<Result<IEnumerable<TResponse>>>
        where TResponse : ProductDTO
        where TRequest : Product;
    

    public class CreateProductCommandHandler<TResponse, TRequest>(
        IProductRepository repository,
        IValidator<TRequest> validator,
        IMapper mapper)
        : IRequestHandler<CreateProductCommand<TResponse, TRequest>, Result<IEnumerable<TResponse>>>
        where TRequest : Product
        where TResponse : ProductDTO
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<TRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<IEnumerable<TResponse>>> Handle(CreateProductCommand<TResponse, TRequest> request, CancellationToken cancellationToken)
        {
            var entities = request.Request.AsQueryable().ProjectTo<TRequest>(mapper.ConfigurationProvider);

            foreach (var entity in entities) 
            {
                var validationResult = await validator.ValidateAsync(entity);

                if (!validationResult.IsValid)
                {
                    return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
                }
            }

            var result = await repository.CreateProductAsync<TResponse, TRequest>(entities);
            return result;
        }
    }
}
