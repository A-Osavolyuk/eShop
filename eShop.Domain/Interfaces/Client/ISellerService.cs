using eShop.Domain.Common.Api;
using eShop.Domain.Requests.Api.Seller;

namespace eShop.Domain.Interfaces.Client;

public interface ISellerService
{
    public ValueTask<Response> RegisterSellerAsync(RegisterSellerRequest request);
}