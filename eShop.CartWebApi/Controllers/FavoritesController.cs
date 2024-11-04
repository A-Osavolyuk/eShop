using eShop.CartWebApi.Commands.Favorites;
using eShop.CartWebApi.Queries.Favorites;
using eShop.Domain.Requests.Favorites;

namespace eShop.CartWebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class FavoritesController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;
    
    [HttpPut("update-favorites")]
    public async ValueTask<ActionResult<ResponseDTO>> UpdateCartAsync([FromBody] UpdateFavoritesRequest request)
    {
        var response = await sender.Send(new UpdateFavoritesCommand(request));

        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
    
    [HttpGet("get-favorites")]
    public async ValueTask<ActionResult<ResponseDTO>> GetCartAsync([FromRoute] GetFavoritesRequest request)
    {
        var response = await sender.Send(new GetFavoritesQuery(request));

        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
}