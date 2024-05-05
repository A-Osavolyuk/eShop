using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Products
{
    public record CreateShoesProductCommand(CreateShoesRequest CreateProductRequest) : IRequest<Result<ProductDTO>>;

    public class CreateShoesProductCommandHandler(
        IProductRepository repository,
        IValidator<CreateShoesRequest> validator,
        IMapper mapper)
        : IRequestHandler<CreateShoesProductCommand, Result<ProductDTO>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<CreateShoesRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<ProductDTO>> Handle(CreateShoesProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.CreateProductRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var result = await repository.CreateProductAsync(mapper.Map<Shoes>(request.CreateProductRequest));
                return result;
            }

            return new(new FailedValidationException("Validation Error(s)", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
