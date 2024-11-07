using eShop.CartApi.Commands.Carts;
using eShop.CartApi.Queries.Carts;
using eShop.Domain.Requests.Cart;

namespace eShop.CartApi.Controllers
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
        
        [HttpGet("get-cart/{userId:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetCartAsync(Guid userId)
        {
            var response = await sender.Send(new GetCartQuery(userId));

            return response.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
    }
}