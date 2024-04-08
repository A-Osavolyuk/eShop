using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace eShop.Infrastructure.Services
{
    public class CategoriesService(
        IHttpClientService clientService,
        IConfiguration configuration) : ICategoriesService
    {
        private readonly IHttpClientService clientService = clientService;
        private readonly IConfiguration configuration = configuration;

        public async ValueTask<ResponseDto> CreateCategoryAsync(CategoryDto Category)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Categories", Data: Category, Method: ApiMethod.POST));
        }

        public async ValueTask<ResponseDto> DeleteCategoryByIdAsync(Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Categories/{Id}", Method: ApiMethod.DELETE));
        }

        public async ValueTask<ResponseDto> GetAllCategoriesAsync()
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Categories", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> GetCategoryByIdAsync(Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Categories/{Id}", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> GetCategoryByNameAsync(string Name)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Categories/{Name}", Method: ApiMethod.GET));
        }

        public async ValueTask<ResponseDto> UpdateCategoryAsync(CategoryDto Category, Guid Id)
        {
            return await clientService.SendAsync(new RequestDto(
                Url: $"{configuration["Services:ProductWebApi"]}/api/v1/Categories/{Id}", Data: Category, Method: ApiMethod.PUT));
        }
    }
}
