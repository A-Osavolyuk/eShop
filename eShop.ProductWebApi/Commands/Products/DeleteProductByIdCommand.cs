using eShop.Application.Extensions;
using eShop.Domain.Common;
using eShop.Domain.DTOs.Requests.Review;
using eShop.Domain.Exceptions;
using eShop.Domain.Requests.Product;
using eShop.Domain.Responses.Product;
using MassTransit;
using MediatR;
using Unit = LanguageExt.Unit;

namespace eShop.ProductWebApi.Commands.Products
{
    public record DeleteProductByIdCommand(DeleteProductRequest Request) : IRequest<Result<DeleteProductResponse>>;

    public class DeleteProductByIdCommandHandler(
        ILogger<DeleteProductByIdCommandHandler> logger,
        ProductDbContext context,
        IRequestClient<DeleteReviewsRequest> requestClient) : IRequestHandler<DeleteProductByIdCommand, Result<DeleteProductResponse>>
    {
        private readonly ILogger<DeleteProductByIdCommandHandler> logger = logger;
        private readonly ProductDbContext context = context;
        private readonly IRequestClient<DeleteReviewsRequest> requestClient = requestClient;

        public async Task<Result<DeleteProductResponse>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var actionMessage = new ActionMessage("delete product with ID {0}", request.Request.Id);
            try
            {
                logger.LogInformation("Attempting to delete product with ID {id}. Request ID {requestID}", request.Request.Id, request.Request.RequestId);

                var product = await context.Products.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == request.Request.Id, cancellationToken: cancellationToken);

                if (product is null)
                {
                    return logger.LogInformationWithException<DeleteProductResponse>(
                        new NotFoundException($"Cannot find product with ID {request.Request.Id}"), 
                        actionMessage, request.Request.RequestId);
                }

                var response = await requestClient.GetResponse<ResponseDTO>(new DeleteReviewsRequest() { Id = request.Request.Id }, cancellationToken);

                if (!response.Message.IsSucceeded)
                {
                    return logger.LogErrorWithException<DeleteProductResponse>(
                        new FailedRpcException(response.Message.ErrorMessage), actionMessage, request.Request.RequestId);
                }

                context.Products.Remove(product);
                await context.SaveChangesAsync(cancellationToken);

                logger.LogInformation("Product with ID {id} was successfully deleted. Request ID {requestId}", request.Request.Id, request.Request.RequestId);

                return new(new DeleteProductResponse()
                {
                    Message = "Product successfully deleted",
                    IsSucceeded = true
                });
            }
            catch (DbUpdateException dbUpdateException)
            {
                return logger.LogErrorWithException<DeleteProductResponse>(dbUpdateException, actionMessage, request.Request.RequestId);
            }
            catch (Exception ex)
            {
                return logger.LogErrorWithException<DeleteProductResponse>(ex, actionMessage, request.Request.RequestId);
            }
        }
    }
}
