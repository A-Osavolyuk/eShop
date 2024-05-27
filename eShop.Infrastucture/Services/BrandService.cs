using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using eShop.Domain.Enums;

namespace eShop.Infrastructure.Services
{
    public class BrandSevice(
        IHttpClientService clientService,
        IConfiguration configuration) : IBrandService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;
        public async ValueTask<ResponseDTO> GetBrandsListAsync() => await clientService.SendAsync(
            new RequestDto(Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Brands", Method: HttpMethods.GET));
    }
}
