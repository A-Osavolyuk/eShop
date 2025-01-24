using eShop.Domain.Common.Api;

namespace eShop.Domain.Interfaces.Client;

public interface IBrandService
{
    public ValueTask<Response> GetBrandsListAsync();
}