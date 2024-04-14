using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Products
{
    public record CreateClothingProductCommand(CreateClothingRequest CreateProductRequest) : IRequest<Result<ProductDTO>>;

    public class CreateClothingProductCommandHandler(
        IProductRepository repository,
        IValidator<CreateClothingRequest> validator,
        IMapper mapper)
        : IRequestHandler<CreateClothingProductCommand, Result<ProductDTO>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<CreateClothingRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<ProductDTO>> Handle(CreateClothingProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CreateProductRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var product = request.CreateProductRequest.ProductType switch
                {
                    ProductType.Clothing => mapper.Map<Clothing>(request.CreateProductRequest),
                    ProductType.Shoes => mapper.Map<Shoes>(request.CreateProductRequest),
                    _ => new Product()
                };

                var result = await repository.CreateProductAsync(product);
                return result;
            }

            return new(new FailedValidationException("Validation Error(s)", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
