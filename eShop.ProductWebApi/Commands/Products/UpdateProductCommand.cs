using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;
namespace eShop.ProductWebApi.Commands.Products
{
    public record UpdateProductCommand(UpdateProductRequest Request) : IRequest<Result<UpdateProductResponse>>;

    public class UpdateProductCommandHandler(
        IProductRepository repository, 
        IValidator<UpdateProductRequest> validator,
        IMapper mapper) 
        : IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IValidator<UpdateProductRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

            if (validationResult.IsValid) 
            {
                var product = request.Request.Category switch
                {
                    Categoty.Clothing => mapper.Map<Clothing>(request.Request),
                    Categoty.Shoes => mapper.Map<Shoes>(request.Request),
                    Categoty.None => new Product(),
                    _ => throw new Exception("Not specified category")
                };

                var result = await repository.UpdateProductAsync(product);
                return result;
            }

            return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
