﻿namespace eShop.ProductApi.Commands.Sellers;

internal sealed record RegisterSellerCommand(RegisterSellerRequest Request) : IRequest<Result<RegisterSellerResponse>>;

internal sealed class RegisterSellerCommandHandler(
    AppDbContext context,
    AuthClient client) : IRequestHandler<RegisterSellerCommand, Result<RegisterSellerResponse>>
{
    private readonly AppDbContext context = context;
    private readonly AuthClient client = client;

    public async Task<Result<RegisterSellerResponse>> Handle(RegisterSellerCommand request, CancellationToken cancellationToken)
    {
        var userResponse = await client.GetUserAsync(request.Request.UserId);

        if (!userResponse.IsSucceeded)
        {
            return new(new NotFoundException(userResponse.Message));
        }
        
        var initiateSellerResponse = await client.InitiateSellerAsync(request.Request.UserId);

        if (!initiateSellerResponse.IsSucceeded)
        {
            return new(new FailedRpcException(initiateSellerResponse.Message));
        }

        var entity = new SellerEntity()
        {
            UserId = Guid.Parse(userResponse.User.Id),
            Name = userResponse.User.Username,
            Email = userResponse.User.Email,
            PhoneNumber = userResponse.User.PhoneNumber,
        };

        await context.Sellers.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new RegisterSellerResponse()
        {
            Message = "Seller was successfully registered"
        };
    }
}