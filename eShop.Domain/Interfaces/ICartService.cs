namespace eShop.Domain.Interfaces
{
    public interface ICartService
    {
        public ValueTask<ResponseDto> GetCartAsync(Guid userId);
        public ValueTask<ResponseDto> UpdateCartAsync(UpdateCartRequest request);
    }
}
