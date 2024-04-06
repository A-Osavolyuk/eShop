using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class SubcategoriesService(
        IHttpClientService clientService,
        IConfiguration configuration) : ISubcategoriesService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDto> CreateSubcategoryAsync(SubcategoryDto Subcategory)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Subcategories", Data: Subcategory, Method: ApiMethod.POST));
        }

        public async ValueTask<ResponseDto> DeleteSubcategoryByIdAsync(Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Subcategories/{Id}", Method: ApiMethod.DELETE));
        }

        public async ValueTask<ResponseDto> GetAllSubcategoriesAsync()
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Subcategories", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> GetSubcategoryByIdAsync(Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Subcategories/{Id}", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> GetSubcategoryByNameAsync(string Name)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Subcategories/{Name}", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> UpdateSubcategoryAsync(SubcategoryDto Subcategory, Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Subcategories/{Id}", Data: Subcategory, Method: ApiMethod.PUT));
        }
    }
}
