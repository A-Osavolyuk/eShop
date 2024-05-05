using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Products
{
    public record UpdateClothingProductCommand(UpdateClothingRequest UpdateProductRequest, Guid Id) : IRequest<Result<ProductDTO>>;

    public class UpdateClothingProductCommandHandler(
        IProductRepository repository,
        IValidator<UpdateClothingRequest> validator,
        IMapper mapper)
        : IRequestHandler<UpdateClothingProductCommand, Result<ProductDTO>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<UpdateClothingRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<ProductDTO>> Handle(UpdateClothingProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.UpdateProductRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var product = request.UpdateProductRequest.ProductType switch
                {
                    ProductType.Clothing => mapper.Map<Clothing>(request.UpdateProductRequest),
                    ProductType.Shoes => mapper.Map<Shoes>(request.UpdateProductRequest),
                    _ => new Product()
                };

                product.Id = request.Id;
                var result = await repository.UpdateProductAsync(product);
                return result;
            }

            return new(new FailedValidationException("Validation Error(s)", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
