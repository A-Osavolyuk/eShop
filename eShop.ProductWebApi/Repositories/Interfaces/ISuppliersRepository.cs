namespace eShop.ProductWebApi.Repositories.Interfaces
{
    public interface ISuppliersRepository
    {
        public ValueTask<Result<IEnumerable<SupplierEntity>>> GetAllSuppliersAsync();
        public ValueTask<Result<SupplierEntity>> GetSupplierByIdAsync(Guid Id);
        public ValueTask<Result<SupplierEntity>> GetSupplierByNameAsync(string Name);
        public ValueTask<Result<SupplierEntity>> CreateSupplierAsync(SupplierEntity Supplier);
        public ValueTask<Result<SupplierEntity>> UpdateSupplierAsync(SupplierEntity Supplier, Guid Id);
        public ValueTask<Result<Unit>> DeleteSupplierByIdAsync(Guid Id);
        public ValueTask<Result<Unit>> ExistsAsync(Guid Id);
    }
}
