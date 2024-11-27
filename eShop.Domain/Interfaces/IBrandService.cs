namespace eShop.Domain.Interfaces
{
    public interface IBrandService
    {
        public ValueTask<ResponseDto> GetBrandsListAsync();
    }
}
