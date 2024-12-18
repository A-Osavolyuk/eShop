using eShop.Domain.Common.Api;

namespace eShop.Domain.Interfaces;

public interface ISellerService
{
    public ValueTask<Response> RegisterSellerAsync(RegisterSellerRequest request);
}