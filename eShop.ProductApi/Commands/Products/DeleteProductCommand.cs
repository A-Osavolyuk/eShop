﻿namespace eShop.ProductApi.Commands.Products;

internal sealed record DeleteProductCommand(DeleteProductRequest Request) : IRequest<Result<DeleteProductResponse>>;

internal sealed class DeleteProductCommandHandler(AppDbContext context) : IRequestHandler<DeleteProductCommand, Result<DeleteProductResponse>>
{
    private readonly AppDbContext context = context;

    public async Task<Result<DeleteProductResponse>> Handle(DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var entity = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Request.ProductId, cancellationToken);

            if (entity is null)
            {
                return new Result<DeleteProductResponse>(new NotFoundException($"Cannot find product with ID {request.Request.ProductId}"));
            }
            
            context.Products.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return new Result<DeleteProductResponse>(new DeleteProductResponse()
            {
                Message = "Product was successfully deleted",
            });

        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return new Result<DeleteProductResponse>(ex);
        }
    }
}