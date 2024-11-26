namespace eShop.AuthApi.Rpc;

public class CartClient(CartService.CartServiceClient client, ILogger<CartClient> logger)
{
    private readonly CartService.CartServiceClient client = client;
    private readonly ILogger<CartClient> logger = logger;

    public async ValueTask<InitiateUserResponse> InitiateUserAsync(InitiateUserRequest request)
    {
        logger.LogInformation("Calling RPC InitiateUser");
        
        var response = await client.InitiateUserAsync(request);
        
        logger.LogInformation("Called RPC InitiateUser");
        return response;
    }
}