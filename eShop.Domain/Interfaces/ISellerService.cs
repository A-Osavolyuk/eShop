namespace eShop.Domain.Interfaces;

public interface ISellerService
{
    public ValueTask<ResponseDto> RegisterSellerAsync(RegisterSellerRequest request);
}