using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Suppliers
{
    public record CreateSupplierCommand(CreateSupplierRequest createSupplierRequest) : IRequest<Result<SupplierDTO>>;

    public class CreateSupplierCommandHandler(
        ISuppliersRepository repository,
        IValidator<CreateSupplierRequest> validator,
        IMapper mapper) : IRequestHandler<CreateSupplierCommand, Result<SupplierDTO>>
    {
        private readonly ISuppliersRepository repository = repository;
        private readonly IValidator<CreateSupplierRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<SupplierDTO>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.createSupplierRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var Supplier = mapper.Map<Supplier>(request.createSupplierRequest);
                var result = await repository.CreateSupplierAsync(Supplier);
                return result;
            }

            return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
