using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;

namespace eShop.Domain.Interfaces
{
    public interface ISuppliersService
    {
        public ValueTask<ResponseDto> GetAllSuppliersAsync();
        public ValueTask<ResponseDto> GetSupplierByIdAsync(Guid Id);
        public ValueTask<ResponseDto> GetSupplierByNameAsync(string Name);
        public ValueTask<ResponseDto> CreateSupplierAsync(SupplierDto Supplier);
        public ValueTask<ResponseDto> UpdateSupplierAsync(SupplierDto Supplier, Guid Id);
        public ValueTask<ResponseDto> DeleteSupplierByIdAsync(Guid Id);
    }
}
