using eShop.Domain.DTOs;

namespace eShop.Domain.Interfaces
{
    public interface IBrandService
    {
        public ValueTask<ResponseDTO> GetBrandsListAsync();
        public ValueTask<ResponseDTO> GetBrandsNamesListAsync();
    }
}
