namespace eShop.Domain.Interfaces.Client;

public interface ISellerService
{
    public ValueTask<Response> RegisterSellerAsync(RegisterSellerRequest request);
}