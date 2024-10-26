using eShop.CartWebApi.Commands.Carts;
using eShop.Domain.Requests.Cart;

namespace eShop.CartWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CartsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpPut("/update-cart")]
        public async Task<ActionResult<ResponseDTO>> UpdateCartAsync([FromBody] UpdateCartRequest request)
        {
            var result = await sender.Send(new UpdateCartCommand(request));
            
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
    }
}