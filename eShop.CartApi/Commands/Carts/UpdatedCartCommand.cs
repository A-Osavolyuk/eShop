﻿namespace eShop.CartApi.Commands.Carts;

internal sealed record UpdatedCartCommand(UpdateCartRequest Request) : IRequest<Result<UpdateCartResponse>>;

internal sealed class UpdatedCartCommandHandler(
    IMongoDatabase database) : IRequestHandler<UpdatedCartCommand, Result<UpdateCartResponse>>
{
    private readonly IMongoDatabase database = database;

    public async Task<Result<UpdateCartResponse>> Handle(UpdatedCartCommand request,
        CancellationToken cancellationToken)
    {
        var cartCollection = database.GetCollection<CartEntity>("Carts");

        var cart = await cartCollection.Find(x => x.CartId == request.Request.CartId)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return new(new NotFoundException($"Cannot find cart with ID {request.Request.CartId}."));
        }
        else
        {
            var newCart = new CartEntity
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                ItemsCount = request.Request.ItemsCount,
                UpdatedAt = DateTime.Now,
                CreatedAt = cart.CreatedAt,
                Items = request.Request.Items
            };

            await cartCollection.ReplaceOneAsync(x => x.CartId == request.Request.CartId, newCart,
                cancellationToken: cancellationToken);

            return new UpdateCartResponse()
            {
                Message = "Cart was successfully updated",
            };
        }
    }
}