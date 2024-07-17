using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;
using Unit = LanguageExt.Unit;
namespace eShop.ProductWebApi.Commands.Products
{
    public record UpdateProductCommand(UpdateProductRequest Request) : IRequest<Result<Unit>>;

    public class UpdateProductCommandHandler(
        IProductRepository repository,
        IValidator<UpdateProductRequest> validator,
        IMapper mapper)
        : IRequestHandler<UpdateProductCommand, Result<Unit>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<UpdateProductRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

            if (validationResult.IsValid)
            {
                var product = request.Request.Category switch
                {
                    Category.Clothing => mapper.Map<Clothing>(request.Request),
                    Category.Shoes => mapper.Map<Shoes>(request.Request),
                    Category.None => new Product(),
                    _ => throw new Exception("Not specified category")
                };

                var result = await repository.UpdateProductAsync(product);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
