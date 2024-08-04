using eShop.CartWebApi.Commands.Carts;

namespace eShop.CartWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CartsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

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
