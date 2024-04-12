using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Exceptions;

namespace eShop.ProductWebApi.Repositories
{
    public class ProductsRepository(ProductDbContext context, IMapper mapper) : IProductRepository
    {
        private readonly ProductDbContext context = context;
        private readonly IMapper mapper = mapper;

        public async ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsListAsync()
        {
            try
            {
                return await context.Products
                    .AsNoTracking()
                    .Select(x => new ProductDTO()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        ProductType = x.ProductType,
                        Price = new Money
                        {
                            Amount = x.Price.Amount,
                            Currency = x.Price.Currency,
                        },
                        Brand = new Brand
                        {
                            Id = x.Brand.Id,
                            Name = x.Brand.Name,
                            Country = x.Brand.Country
                        },
                        Supplier = new Supplier
                        {
                            Id = x.Supplier.Id,
                            Name = x.Supplier.Name,
                            ContactEmail = x.Supplier.ContactEmail,
                            ContactPhone = x.Supplier.ContactPhone
                        },
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsListByTypeAsync(ProductType Type)
        {
            try
            {
                return Type switch
                {
                    ProductType.Clothing => new(await context.Products.AsNoTracking().OfType<Clothing>()
                                                .ProjectTo<ClothingDTO>(mapper.ConfigurationProvider).ToListAsync()),

                    ProductType.Shoes => new(await context.Products.AsNoTracking().OfType<Shoes>()
                                                .ProjectTo<ShoesDTO>(mapper.ConfigurationProvider).ToListAsync()),

                    _ => new(Enumerable.Empty<ProductDTO>())
                };
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductDTO>> GetProductByIdAsync(Guid Id)
        {
            try
            {
                var product = await context.Products.AsNoTracking().ProjectTo<ProductDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == Id);

                return product is null ? new(new NotFoundProductException(Id)) : new(product);
            }
            catch (Exception ex)
            {
                return new(ex);
            }
        }
    }

    public interface IProductRepository
    {
        public ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsListAsync();
        public ValueTask<Result<IEnumerable<ProductDTO>>> GetProductsListByTypeAsync(ProductType type);
        public ValueTask<Result<ProductDTO>> GetProductByIdAsync(Guid Id);
    }
}
