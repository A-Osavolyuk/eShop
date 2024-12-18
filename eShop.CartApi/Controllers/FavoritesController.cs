using eShop.Domain.Common.Api;
using eShop.Domain.Requests.CartApi.Favorites;
using Response = eShop.Domain.Common.Api.Response;

namespace eShop.CartApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class FavoritesController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;
    
    [HttpPut("update-favorites")]
    public async ValueTask<ActionResult<Response>> UpdateCartAsync([FromBody] UpdateFavoritesRequest request)
    {
        var response = await sender.Send(new UpdateFavoritesCommand(request));

        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpGet("get-favorites/{userId:guid}")]
    public async ValueTask<ActionResult<Response>> GetCartAsync(Guid userId)
    {
        var response = await sender.Send(new GetFavoritesQuery(userId));

        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
}