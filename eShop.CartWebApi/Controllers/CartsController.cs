using eShop.CartWebApi.Commands.Carts;
using eShop.CartWebApi.Queries.Carts;
using eShop.Domain.Requests.Cart;

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
            var response = await sender.Send(new UpdatedCartCommand(request));

            return response.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                    ExceptionHandler.HandleException);
        }
        
        [HttpGet("get-cart")]
        public async ValueTask<ActionResult<ResponseDTO>> GetCartAsync([FromBody] GetCartRequest request)
        {
            var response = await sender.Send(new GetCartQuery(request));

            return response.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
    }
}