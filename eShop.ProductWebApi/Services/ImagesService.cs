using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.ProductWebApi.Exceptions;
using LanguageExt;

namespace eShop.ProductWebApi.Services
{
    public class ImagesService(ProductDbContext context, ILogger<ImagesService> logger, IMapper mapper) : IImagesService
    {
        private readonly ProductDbContext context = context;
        private readonly ILogger<ImagesService> logger = logger;
        private readonly IMapper mapper = mapper;

        public async ValueTask<Result<Unit>> AddImagesToProduct(IFormFileCollection images, Guid ProductId)
        {
            try
            {
                logger.LogInformation($"Trying to add images to product with id: {ProductId}.");

                var product = await context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == ProductId);

                if (product is not null)
                {
                    logger.LogInformation($"Creating images.");

                    var imagesList = new List<ProductImage>();

                    for (int i = 0; i < images.Count; i++)
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            await images[i].CopyToAsync(stream);
                            imagesList.Add(new ProductImage()
                            {
                                Id = Guid.NewGuid(),
                                Name = $"product_{product.Id}_{i}.{images[i].FileName.Split('.')[1]}",
                                Image = stream.ToArray(),
                                ProductId = product.Id,
                                VariantId = product.VariantId
                            });
                        }
                    }

                    await context.ProductImages.AddRangeAsync(imagesList);
                    await context.SaveChangesAsync();

                    logger.LogInformation($"Images successfully created and added to product with id: {ProductId}.");

                    return (new Unit());
                }

                return new(new NotFoundProductException(ProductId));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on adding images to product with id: {ProductId} with error message: {ex.Message}.");
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteImageById(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to delete an image with id: {Id}.");

                var image = await context.ProductImages
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if (image is not null)
                {
                    context.ProductImages.Remove(image);
                    await context.SaveChangesAsync();

                    logger.LogInformation($"Successfully deleted an image with id: {Id}.");

                    return new(new Unit());
                }

                return new(new NotFoundImagesException($"Cannot find an image with id: {Id}."));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on deleting an image with id: {Id} with error message: {ex.Message}.");
                return new(ex);
            }
        }

        public async ValueTask<Result<Unit>> DeleteImagesById(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to delete images with id: {Id}");

                var images = await context.ProductImages
                    .AsNoTracking()
                    .Where(x => x.Id == Id)
                    .ToListAsync();

                if (images is not null)
                {
                    context.ProductImages.RemoveRange(images);
                    await context.SaveChangesAsync();

                    logger.LogInformation($"Successfully deleted images with id: {Id}");

                    return new(new Unit());
                }

                return new(new NotFoundImagesException($"Cannot find an image with id: {Id}"));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on deleting images with id: {Id} with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<ProductImageDTO>> GetImageById(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to get images by id: {Id}");

                var image = await context.ProductImages
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == Id);

                if (image is not null)
                {
                    logger.LogInformation($"Successfully found images by id: {Id}");
                    return new(mapper.Map<ProductImageDTO>(image));
                }

                return new(new NotFoundImagesException($"Cannot find any images with id: {Id}"));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting images by id: {Id} with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<ProductImageDTO>>> GetImagesByProductId(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to get images by product id: {Id}");

                var images = await context.ProductImages
                    .AsNoTracking()
                    .Where(x => x.ProductId == Id)
                    .ProjectTo<ProductImageDTO>(mapper.ConfigurationProvider)
                    .ToListAsync();

                if (images.Any())
                {
                    logger.LogInformation($"Successfully found images by product id: {Id}");
                    return new(images);
                }

                return new(new NotFoundImagesException($"Cannot find any images with product id: {Id}"));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting images by product id: {Id} with error message: {ex.Message}");
                return new(ex);
            }
        }

        public async ValueTask<Result<IEnumerable<ProductImageDTO>>> GetImagesByVariantId(Guid Id)
        {
            try
            {
                logger.LogInformation($"Trying to get images by variant id: {Id}");

                var images = await context.ProductImages
                    .AsNoTracking()
                    .Where(x => x.VariantId == Id)
                    .ProjectTo<ProductImageDTO>(mapper.ConfigurationProvider)
                    .ToListAsync();

                if (images.Any())
                {
                    logger.LogInformation($"Successfully found images by variant id: {Id}");
                    return new(images);
                }

                return new(new NotFoundImagesException($"Cannot find any images with variant id: {Id}"));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed on getting images by variant id: {Id} with error message: {ex.Message}");
                return new(ex);
            }
        }
    }
    public interface IImagesService
    {
        public ValueTask<Result<IEnumerable<ProductImageDTO>>> GetImagesByProductId(Guid Id);
        public ValueTask<Result<ProductImageDTO>> GetImageById(Guid Id);
        public ValueTask<Result<IEnumerable<ProductImageDTO>>> GetImagesByVariantId(Guid Id);
        public ValueTask<Result<Unit>> AddImagesToProduct(IFormFileCollection images, Guid ProductId);
        public ValueTask<Result<Unit>> DeleteImageById(Guid Id);
        public ValueTask<Result<Unit>> DeleteImagesById(Guid Id);
    }
}
