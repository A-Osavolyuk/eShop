using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Product;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Commands.Products
{
    public record CreateProductCommand(IEnumerable<CreateProductRequest> CreateRequest) : IRequest<Result<Unit>>;
    

    public class CreateProductCommandHandler(
        IProductRepository repository,
        IValidator<CreateProductRequest> validator,
        IMapper mapper) : IRequestHandler<CreateProductCommand, Result<Unit>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<CreateProductRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<Unit>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            foreach (var product in request.CreateRequest) 
            {
                var validationResult = await validator.ValidateAsync(product);

                if (!validationResult.IsValid)
                {
                    return new(new FailedValidationException(validationResult.Errors));
                }
            }

            var entities = request.CreateRequest.First().Category switch
            {
                Category.Clothing => request.CreateRequest.AsQueryable().ProjectTo<Clothing>(mapper.ConfigurationProvider),
                Category.Shoes => request.CreateRequest.AsQueryable().ProjectTo<Shoes>(mapper.ConfigurationProvider),
                Category.None => Enumerable.Empty<Product>(),
                _ => throw new NotImplementedException()
            };

            var result = await repository.CreateProductAsync(entities);
            return result;
        }
    }
}
