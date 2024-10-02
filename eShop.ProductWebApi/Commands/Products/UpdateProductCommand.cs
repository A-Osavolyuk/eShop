using AutoMapper;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Product;
using eShop.ProductWebApi.Exceptions;
using FluentValidation;
using MediatR;
using Unit = LanguageExt.Unit;
namespace eShop.ProductWebApi.Commands.Products
{
    public record UpdateProductCommand(UpdateProductRequest Request) : IRequest<Result<Unit>>;

    public class UpdateProductCommandHandler(
        IValidator<UpdateProductRequest> validator,
        IMapper mapper,
        ILogger<UpdateProductCommandHandler> logger,
        ProductDbContext context)
        : IRequestHandler<UpdateProductCommand, Result<Unit>>
    {
        private readonly IValidator<UpdateProductRequest> validator = validator;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<UpdateProductCommandHandler> logger = logger;
        private readonly ProductDbContext context = context;

        public async Task<Result<Unit>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("update product with ID {0}", request.Request.Id);

            try
            {
                logger.LogInformation("Attempting to update product with ID {id}. Request ID {requestID}", request.Request.Id, request.Request.RequestId);

                var validationResult = await validator.ValidateAsync(request.Request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    return logger.LogErrorWithException<Unit>(new FailedValidationException(validationResult.Errors), actionMessage, request.Request.RequestId);
                }

                var product = request.Request.Category switch
                {
                    Category.Clothing => mapper.Map<Clothing>(request.Request),
                    Category.Shoes => mapper.Map<Shoes>(request.Request),
                    Category.None => new Product(),
                    _ => throw new Exception("Not specified category")
                };

                var productExists = await context.Products.AsNoTracking().AnyAsync(_ => _.Id == product.Id && _.Category == product.Category);

                if (!productExists)
                {
                    return logger.LogErrorWithException<Unit>(new NotFoundProductException(request.Request.Id), actionMessage, request.Request.RequestId);
                }

                var bransExists = await context.Brands.AsNoTracking().AnyAsync(_ => _.Id == product.BrandId);

                if (!bransExists)
                {
                    return logger.LogErrorWithException<Unit>(new NotFoundBrandException(request.Request.BrandId), actionMessage, request.Request.RequestId);
                }

                context.Products.Update(product);
                await context.SaveChangesAsync();

                logger.LogInformation("Product with ID {id} was successfully updated. Request ID {requestID}", request.Request.Id, request.Request.RequestId);

                return new(new Unit());
            }
            catch (DbUpdateException dbUpdateException)
            {
                return logger.LogErrorWithException<Unit>(dbUpdateException, actionMessage, request.Request.RequestId);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<Unit>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
