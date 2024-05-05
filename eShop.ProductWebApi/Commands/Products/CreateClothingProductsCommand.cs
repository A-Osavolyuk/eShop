using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Domain.DTOs.Requests;
using eShop.ProductWebApi.Repositories;
using MediatR;

namespace eShop.ProductWebApi.Commands.Products
{
    public record CreateClothingProductsCommand(CreateClothingRequest CreateClothingRequest) : IRequest<Result<IEnumerable<ProductDTO>>>;

    public class CreateClothingProductsCommandHandler(IProductRepository repository, IMapper mapper) : IRequestHandler<CreateClothingProductsCommand, Result<IEnumerable<ProductDTO>>>
    {
        private readonly IProductRepository repository = repository;
        private readonly IMapper mapper = mapper;

        public async Task<Result<IEnumerable<ProductDTO>>> Handle(CreateClothingProductsCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.CreateProductsAsync(request.CreateClothingRequest);
            return result;
        }
    }
}
