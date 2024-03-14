namespace eShop.ProductWebApi.Suppliers.Create
{
    public record CreateSupplierCommand(CreateUpdateSupplierRequestDto Supplier) : IRequest<Result<SupplierDto>>;

    public class CreateSupplierCommandHandler(
        ISuppliersRepository repository, 
        IMapper mapper, 
        IValidator<CreateUpdateSupplierRequestDto> validator) 
        : IRequestHandler<CreateSupplierCommand, Result<SupplierDto>>
    {
        private readonly ISuppliersRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IValidator<CreateUpdateSupplierRequestDto> validator = validator;

        public async Task<Result<SupplierDto>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Supplier, cancellationToken);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).", 
                    validationResult.Errors.Select(e => e.ErrorMessage)));

            var supplier = mapper.Map<SupplierEntity>(request.Supplier);
            var result = await repository.CreateSupplierAsync(supplier);
            return result.Match<Result<SupplierDto>>(s => new(mapper.Map<SupplierDto>(s)), f => new(f));
        }
    }
}
