using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class SuppliersService(
        IHttpClientService clientService,
        IConfiguration configuration) : ISuppliersService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDto> CreateSupplierAsync(SupplierDto Supplier)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Suppliers", Data: Supplier, Method: ApiMethod.POST));
        }

        public async ValueTask<ResponseDto> DeleteSupplierByIdAsync(Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Suppliers/{Id}", Method: ApiMethod.DELETE));
        }

        public async ValueTask<ResponseDto> GetAllSuppliersAsync()
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Suppliers", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> GetSupplierByIdAsync(Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Suppliers/{Id}", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> GetSupplierByNameAsync(string Name)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Suppliers/{Name}", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> UpdateSupplierAsync(SupplierDto Supplier, Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Suppliers/{Id}", Data: Supplier, Method: ApiMethod.PUT));
        }
    }
}
