using AutoMapper;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Brand;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Brands
{
    public record UpdateBrandCommand(UpdateBrandRequest UpdateBrandRequest) : IRequest<Result<BrandDTO>>;

    public class UpdateBrandCommandHandler(
        IBrandsRepository repository,
        IValidator<UpdateBrandRequest> validator,
        IMapper mapper) : IRequestHandler<UpdateBrandCommand, Result<BrandDTO>>
    {
        private readonly IBrandsRepository repository = repository;
        private readonly IValidator<UpdateBrandRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<BrandDTO>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.UpdateBrandRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var brand = mapper.Map<Brand>(request.UpdateBrandRequest);
                var result = await repository.UpdateBrandAsync(brand);
                return result;
            }

            return new(new FailedValidationException(validationResult.Errors));
        }
    }
}
