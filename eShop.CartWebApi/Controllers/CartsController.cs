namespace eShop.CartWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CartsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpPost("add-good-to-cart")]
        public async ValueTask<ActionResult<ResponseDTO>> AddGoodToCartAsync([FromBody] AddGoodToCartRequest request)
        {
            var result = await sender.Send(new AddGoodToCartCommand(request));
            return result.Match(
                s => new ResponseBuilder().Succeeded().WithResultMessage("Good was successfully added to cart.").Build(),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("create-cart")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateCartAsync([FromBody] CreateCartRequest request)
        {
            var result = await sender.Send(new CreateCartCommand(request));
            return result.Match(
                s => new ResponseBuilder().Succeeded().WithResultMessage("Cart was successfully created.").Build(),
                f => ExceptionHandler.HandleException(f)!);
        }

        [HttpGet("get-cart/{UserId:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetCartByIdAsync(Guid UserId)
        {
            var result = await sender.Send(new GetCartByUserIdQuery(UserId));
            return result.Match(
                s => new ResponseBuilder().Succeeded().WithResult(s).Build(),
                f => ExceptionHandler.HandleException(f)!);
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
