using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Requests.Brand;
using eShop.ProductWebApi.Exceptions;
using MediatR;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Commands.Brands
{
    public record DeleteBrandCommand(DeleteBrandRequest Request) : IRequest<Result<Unit>>;

    public class DeleteBrandCommandHandler(
        ProductDbContext context,
        ILogger<DeleteBrandCommandHandler> logger) : IRequestHandler<DeleteBrandCommand, Result<Unit>>
    {
        private readonly ProductDbContext context = context;
        private readonly ILogger<DeleteBrandCommandHandler> logger = logger;

        public async Task<Result<Unit>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("delete brand with ID {0}", request.Request.Id);
            try
            {
                logger.LogInformation("Attempting to delete brand with ID: {id}. Request ID {requestID}", request.Request.Id, request.Request.RequestId);
                var brand = await context.Brands.AsNoTracking().FirstOrDefaultAsync(_ => _.Id == request.Request.Id);

                if (brand is null)
                {
                    return logger.LogErrorWithException<Unit>(new NotFoundBrandException(request.Request.Id), actionMessage, request.Request.RequestId);
                }

                context.Brands.Remove(brand);
                await context.SaveChangesAsync();

                logger.LogInformation("Successfully deleted brand with ID {Id}. Request ID {requestId}", request.Request.Id, request.Request.RequestId);
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
