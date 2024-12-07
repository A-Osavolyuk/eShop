﻿namespace eShop.CartApi.Queries.Carts;

internal sealed record GetCartQuery(Guid UserId) : IRequest<Result<CartDto>>;

internal sealed class GetCartQueryHandler(
    IMongoDatabase database,
    ILogger<GetCartQueryHandler> logger) : IRequestHandler<GetCartQuery, Result<CartDto>>
{
    private readonly ILogger<GetCartQueryHandler> logger = logger;

    public async Task<Result<CartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        var collection = database.GetCollection<CartEntity>("Carts");
        var cart = await collection.Find(x => x.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return new(new NotFoundException($"Cannot find cart with user ID {request.UserId}."));
        }

        return CartMapper.ToCartDto(cart);
    }
}