using eShop.ProductWebApi.Products.Create;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateProduct([FromBody] ProductDto Product)
        {
            var result = await sender.Send(new CreateProductCommand(Product));

            return result.Match<ActionResult<ResponseDto>>(
                s => CreatedAtAction("", new { Id = s.ProductId }, new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Product was successfully created.")
                    .AddResult(s)
                    .Build()),
                f =>
                {
                    if (f is FailedValidationException exception)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(exception.Message)
                            .AddErrors(exception.Errors.ToList())
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
        }
    }
}
