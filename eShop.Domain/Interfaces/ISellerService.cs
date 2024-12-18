using eShop.Domain.Common.Api;
using eShop.Domain.Requests.ProductApi.Seller;

namespace eShop.Domain.Interfaces;

public interface ISellerService
{
    public ValueTask<Response> RegisterSellerAsync(RegisterSellerRequest request);
}