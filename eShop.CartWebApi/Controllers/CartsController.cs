using eShop.CartWebApi.Commands.Carts;
using eShop.CartWebApi.Queries.Carts;

namespace eShop.CartWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CartsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet("get-cart-by-user-id/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetCartByUserIdAsync(Guid Id)
        {
            var result = await sender.Send(new GetCartByUserIdQuery(Id));

            return result.Match(
            s => new ResponseBuilder().Succeeded().WithResult(s).Build(),
            f => ExceptionHandler.HandleException(f));
        }

    [HttpPut("update-cart")]
    public async ValueTask<ActionResult<ResponseDTO>> UpdateCartAsync([FromBody] UpdateCartRequest request)
    {
        var result = await sender.Send(new UpdateCartCommand(request));
        return result.Match(
            s => new ResponseBuilder().Succeeded().WithResultMessage("Cart was successfully updated.").Build(),
            f => ExceptionHandler.HandleException(f));
    }
}
}
