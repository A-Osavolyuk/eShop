using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using LanguageExt;
using MediatR;

namespace eShop.ProductWebApi.Commands.Products
{
    public record CreateProductCommand(IEnumerable<CreateProductRequest> CreateRequest) : IRequest<Result<CreateProductResponse>>;
    

    public class CreateProductCommandHandler(
        IProductRepository repository,
        IValidator<CreateProductRequest> validator,
        IMapper mapper) : IRequestHandler<CreateProductCommand, Result<CreateProductResponse>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<CreateProductRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            foreach (var product in request.CreateRequest) 
            {
                var validationResult = await validator.ValidateAsync(product);

                if (!validationResult.IsValid)
                {
                    return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
                }
            }

            var entities = request.CreateRequest.First().Category switch
            {
                Categoty.Clothing => request.CreateRequest.AsQueryable().ProjectTo<Clothing>(mapper.ConfigurationProvider),
                Categoty.Shoes => request.CreateRequest.AsQueryable().ProjectTo<Shoes>(mapper.ConfigurationProvider),
                Categoty.None => Enumerable.Empty<Product>(),
                _ => throw new NotImplementedException()
            };

            var result = await repository.CreateProductAsync(entities);
            return result;
        }
    }
}
