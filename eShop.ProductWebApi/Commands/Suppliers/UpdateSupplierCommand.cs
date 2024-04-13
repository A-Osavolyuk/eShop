using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Exceptions;
using eShop.ProductWebApi.Repositories;
using FluentValidation;
using MediatR;

namespace eShop.ProductWebApi.Commands.Suppliers
{
    public record UpdateSupplierCommand(UpdateSupplierRequest UpdateSupplierRequest) : IRequest<Result<SupplierDTO>>;

    public class UpdateSupplierCommandHandler(
        ISuppliersRepository repository,
        IValidator<UpdateSupplierRequest> validator,
        IMapper mapper) : IRequestHandler<UpdateSupplierCommand, Result<SupplierDTO>>
    {
        private readonly ISuppliersRepository repository = repository;
        private readonly IValidator<UpdateSupplierRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<SupplierDTO>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.UpdateSupplierRequest, cancellationToken);

            if (validationResult.IsValid)
            {
                var Supplier = mapper.Map<Supplier>(request.UpdateSupplierRequest);
                var result = await repository.UpdateSupplierAsync(Supplier);
                return result;
            }

            return new(new FailedValidationException("Validation Error(s).", validationResult.Errors.Select(x => x.ErrorMessage)));
        }
    }
}
