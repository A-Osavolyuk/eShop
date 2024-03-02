
namespace eShop.ProductWebApi.Suppliers.Create
{
    public record CreateSupplierCommand(SupplierDto Supplier) : IRequest<Result<SupplierEntity>>;

    public class CreateSupplierCommandHandler(
        ISuppliersRepository repository, 
        IMapper mapper, 
        IValidator<SupplierDto> validator) 
        : IRequestHandler<CreateSupplierCommand, Result<SupplierEntity>>
    {
        private readonly ISuppliersRepository repository = repository;
        private readonly IMapper mapper = mapper;
        private readonly IValidator<SupplierDto> validator = validator;

        public async Task<Result<SupplierEntity>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request.Supplier, cancellationToken);

            if (!validationResult.IsValid)
                return new(new FailedValidationException("Validation Error(s).", 
                    validationResult.Errors.Select(e => e.ErrorMessage)));

            var supplier = mapper.Map<SupplierEntity>(request.Supplier);
            var result = await repository.CreateSupplierAsync(supplier);
            return result.Match<Result<SupplierEntity>>(s => new(s), f => new(f));
        }
    }
}
