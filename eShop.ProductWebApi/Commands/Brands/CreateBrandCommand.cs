using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Brands
{
    public record CreateBrandCommand(CreateBrandRequest createBrandRequest) : IRequest<Result<BrandDTO>>;

    public class CreateBrandCommandHandler(
        IBrandsRepository repository,
        IValidator<CreateBrandRequest> validator,
        IMapper mapper) : IRequestHandler<CreateBrandCommand, Result<BrandDTO>>
    {
        private readonly IBrandsRepository repository = repository;
        private readonly IValidator<CreateBrandRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<BrandDTO>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.createBrandRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var brand = mapper.Map<Brand>(request.createBrandRequest);
                var result = await repository.CreateBrandAsync(brand);
                return result;
            }

            return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
