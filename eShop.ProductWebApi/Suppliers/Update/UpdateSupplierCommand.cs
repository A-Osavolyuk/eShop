namespace eShop.ProductWebApi.Suppliers.Update
{
    public record UpdateSupplierCommand(Guid Id, CreateUpdateSupplierRequest Supplier) : IRequest<Result<SupplierDto>>;

    public class UpdateSupplierCommandHandler(
        ISuppliersRepository repository, 
        IMapper mapper, 
        IValidator<CreateUpdateSupplierRequest> validator) 
        : IRequestHandler<UpdateSupplierCommand, Result<SupplierDto>>
    {
        private readonly ISuppliersRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IValidator<CreateUpdateSupplierRequest> validator = validator;

        public async Task<Result<SupplierDto>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Supplier, cancellationToken);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).", 
                    validationResult.Errors.Select(e => e.ErrorMessage)));

            var supplier = mapper.Map<SupplierEntity>(request.Supplier);
            var result = await repository.UpdateSupplierAsync(supplier, request.Id);

            return result.Match<Result<SupplierDto>>(s => new(mapper.Map<SupplierDto>(s)), f => new(f));
        }
    }
}
