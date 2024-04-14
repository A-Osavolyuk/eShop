using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Products
{
    public record UpdateShoesProductCommand(UpdateShoesRequest UpdateProductRequest) : IRequest<Result<ProductDTO>>;

    public class UpdateShoesProductCommandHandler(
        IProductRepository repository,
        IValidator<UpdateShoesRequest> validator,
        IMapper mapper)
        : IRequestHandler<UpdateShoesProductCommand, Result<ProductDTO>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<UpdateShoesRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<ProductDTO>> Handle(UpdateShoesProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.UpdateProductRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var product = request.UpdateProductRequest.ProductType switch
                {
                    ProductType.Clothing => mapper.Map<Shoes>(request.UpdateProductRequest),
                    ProductType.Shoes => mapper.Map<Shoes>(request.UpdateProductRequest),
                    _ => new Product()
                };

                var result = await repository.UpdateProductAsync(product);
                return result;
            }

            return new(new FailedValidationException("Validation Error(s)", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
