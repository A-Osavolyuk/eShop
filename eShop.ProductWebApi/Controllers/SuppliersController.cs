using eShop.ProductWebApi.Suppliers.Create;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SuppliersController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet]
        public async ValueTask<ActionResult<ResponseDto>> GetSuppliersList()
        {
            return Ok();
        }

        [HttpGet("getSupplierById/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> GetSupplierById(Guid Id)
        {
            return Ok();
        }

        [HttpGet("getSupplierById/{Name}")]
        public async ValueTask<ActionResult<ResponseDto>> GetSupplierByName(string Name)
        {
            return Ok();
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateSupplier([FromBody] SupplierDto supplier)
        {
            var result = await sender.Send(new CreateSupplierCommand(supplier));

            return result.Match<ActionResult<ResponseDto>>(
                s => CreatedAtAction(nameof(GetSupplierById), new { Id = s.SupplierId }, new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Supplier was successfully added.")
                    .AddResult(s)
                    .Build()),
                f =>
                {
                    if (f is FailedValidationException exception)
                    {
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(exception.Message)
                            .AddErrors(exception.Errors.ToList())
                            .Build());
                    }

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddResultMessage(f.Message)
                        .Build());
                });
        }
    }
}
