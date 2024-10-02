using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.DTOs.Requests.Review;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Product;
using eShop.ProductWebApi.Exceptions;
using MassTransit;
using MediatR;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Commands.Products
{
    public record DeleteProductByIdCommand(DeleteProductRequest Request) : IRequest<Result<Unit>>;

    public class DeleteProductByIdCommandHandler(
        ILogger<DeleteProductByIdCommandHandler> logger,
        ProductDbContext context,
        IRequestClient<DeleteReviewsRequest> requestClient) : IRequestHandler<DeleteProductByIdCommand, Result<Unit>>
    {
        private readonly ILogger<DeleteProductByIdCommandHandler> logger = logger;
        private readonly ProductDbContext context = context;
        private readonly IRequestClient<DeleteReviewsRequest> requestClient = requestClient;

        public async Task<Result<Unit>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("delete product with ID {0}", request.Request.Id);
            try
            {
                logger.LogInformation("Attempting to delete product with ID {id}. Request ID {requestID}", request.Request.Id, request.Request.RequestId);

                var product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Request.Id);

                if (product is null)
                {
                    return logger.LogErrorWithException<Unit>(new NotFoundProductException(request.Request.Id), actionMessage, request.Request.RequestId);
                }

                var response = await requestClient.GetResponse<ResponseDTO>(new DeleteReviewsRequest() { Id = request.Request.Id });

                if (!response.Message.IsSucceeded)
                {
                    return logger.LogErrorWithException<Unit>(new FailedRpcException(response.Message.ErrorMessage), actionMessage, request.Request.RequestId);
                }

                context.Products.Remove(product);
                await context.SaveChangesAsync();

                logger.LogInformation("Product with ID {id} was successfully deleted. Request ID {requestId}", request.Request.Id, request.Request.RequestId);

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
