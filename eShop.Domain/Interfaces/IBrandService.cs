using eShop.Domain.Common.Api;

namespace eShop.Domain.Interfaces;

public interface IBrandService
{
    public ValueTask<Response> GetBrandsListAsync();
}