using AutoMapper;
using AutoMapper.QueryableExtensions;
using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Enums;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Product;
using eShop.ProductWebApi.Exceptions;
using FluentValidation;
using LanguageExt;
using MediatR;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Commands.Products
{
    public record CreateProductCommand(IEnumerable<CreateProductRequest> Requests) : IRequest<Result<Unit>>;

    public class CreateProductCommandHandler(
        ProductDbContext context,
        ILogger<CreateProductCommandHandler> logger,
        IValidator<CreateProductRequest> validator,
        IMapper mapper) : IRequestHandler<CreateProductCommand, Result<Unit>>
    {
        private readonly ProductDbContext context = context;
        private readonly ILogger<CreateProductCommandHandler> logger = logger;
        private readonly IValidator<CreateProductRequest> validator = validator;
        private readonly IMapper mapper = mapper;

        public async Task<Result<Unit>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("create product(s)");
            try
            {
                logger.LogInformation($"Attempting to create product(s).");

                foreach (var product in request.Requests)
                {
                    var validationResult = await validator.ValidateAsync(product, cancellationToken);

                    if (!validationResult.IsValid)
                    {
                        return logger.LogErrorWithException<Unit>(new FailedValidationException(validationResult.Errors), actionMessage);
                    }
                }

                var products = request.Requests.First().Category switch
                {
                    Category.Clothing => request.Requests.AsQueryable().ProjectTo<Clothing>(mapper.ConfigurationProvider),
                    Category.Shoes => request.Requests.AsQueryable().ProjectTo<Shoes>(mapper.ConfigurationProvider),
                    Category.None => Enumerable.Empty<Product>(),
                    _ => throw new NotImplementedException("Not implemented creation of product type")
                };
                
                var productsList = products.ToList();
                
                if (!productsList.Any())
                {
                    return logger.LogErrorWithException<Unit>(new EmptyRequestException(), actionMessage);
                }

                var brandExists = await context.Brands.AsNoTracking().AnyAsync(_ => _.Id == productsList.First().BrandId, cancellationToken);

                if (!brandExists)
                {
                    return logger.LogErrorWithException<Unit>(new NotFoundBrandException(productsList.First().BrandId), actionMessage);
                }

                await context.Products.AddRangeAsync(productsList, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                logger.LogInformation($"Product(s) was(were) successfully created.");

                return new(new Unit());
            }
            catch (DbUpdateException dbUpdateException)
            {
                return logger.LogErrorWithException<Unit>(dbUpdateException, actionMessage);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<Unit>(ex, actionMessage);
            }
        }
    }
}
