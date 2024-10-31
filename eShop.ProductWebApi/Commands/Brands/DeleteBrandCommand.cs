using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Brand;
using eShop.Domain.Responses.Brand;
using MediatR;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Commands.Brands
{
    public record DeleteBrandCommand(DeleteBrandRequest Request) : IRequest<Result<DeleteBrandResponse>>;

    public class DeleteBrandCommandHandler(
        ProductDbContext context,
        ILogger<DeleteBrandCommandHandler> logger) : IRequestHandler<DeleteBrandCommand, Result<DeleteBrandResponse>>
    {
        private readonly ProductDbContext context = context;
        private readonly ILogger<DeleteBrandCommandHandler> logger = logger;

        public async Task<Result<DeleteBrandResponse>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("delete brand with ID {0}", request.Request.Id);
            try
            {
                logger.LogInformation("Attempting to delete brand with ID: {id}. Request ID {requestID}", request.Request.Id, request.Request.RequestId);
                var brand = await context.Brands.AsNoTracking()
                    .FirstOrDefaultAsync(_ => _.Id == request.Request.Id, cancellationToken: cancellationToken);

                if (brand is null)
                {
                    return logger.LogInformationWithException<DeleteBrandResponse>(
                        new NotFoundException($"Cannot find brand with ID {request.Request.Id}."), 
                        actionMessage, request.Request.RequestId);
                }

                context.Brands.Remove(brand);
                await context.SaveChangesAsync(cancellationToken);

                logger.LogInformation("Successfully deleted brand with ID {Id}. Request ID {requestId}", request.Request.Id, request.Request.RequestId);
                return new(new DeleteBrandResponse()
                {
                    Message = "Brand was successfully deleted.",
                    IsSucceeded = true
                });
            }
            catch (DbUpdateException dbUpdateException)
            {
                return logger.LogErrorWithException<DeleteBrandResponse>(dbUpdateException, actionMessage, request.Request.RequestId);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<DeleteBrandResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
