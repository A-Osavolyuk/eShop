using eShop.Domain.Entities;
using eShop.Domain.Enums;
using eShop.Domain.Interfaces;
using eShop.ProductWebApi;

namespace eShop.Domain.DTOs.Requests
{
    public class CreateShoesRequest : ProductRequestBase, IVariable
    {
        public CreateShoesRequest() => ProductType = ProductType.Shoes;
        public List<ProductSize> Sizes { get; set; } = new();
        public List<ProductColor> Colors { get; set; } = new();
        public Audience Audience { get; set; }

        public IEnumerable<Product> CreateVariants()
        {
            var clothingRequest = this;
            var variantId = Guid.NewGuid();

            foreach (var color in clothingRequest!.Colors)
            {
                yield return new Shoes()
                {
                    Audience = clothingRequest.Audience,
                    Article = Utilities.ArticleGenerator(),
                    VariantId = variantId,
                    BrandId = clothingRequest.BrandId,
                    SupplierId = clothingRequest.SupplierId,
                    Color = color,
                    Compound = clothingRequest.Compound,
                    Description = clothingRequest.Description,
                    Name = clothingRequest.Name,
                    Amount = clothingRequest.Amount,
                    Currency = clothingRequest.Currency,
                    ProductType = clothingRequest.ProductType,
                    Sizes = clothingRequest.Sizes
                };
            }
        }
    }
}
