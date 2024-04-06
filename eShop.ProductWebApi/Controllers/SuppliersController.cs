using eShop.Domain.DTOs;

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
            var result = await sender.Send(new GetSuppliersListQuery());

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f => StatusCode(500, new ResponseBuilder()
                    .Failed()
                    .AddErrorMessage(f.Message)
                    .Build()));
        }

        [HttpGet("getSupplierById/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> GetSupplierById(Guid Id)
        {
            var result = await sender.Send(new GetSupplierByIdQuery(Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f =>
                {
                    if (f is NotFoundSupplierException ex)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(ex.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddResultMessage(f.Message)
                        .Build());
                });
        }

        [HttpGet("getSupplierByName/{Name}")]
        public async ValueTask<ActionResult<ResponseDto>> GetSupplierByName(string Name)
        {
            var result = await sender.Send(new GetSupplierByNameQuery(Name));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f =>
                {
                    if (f is NotFoundSupplierException ex)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(ex.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddResultMessage(f.Message)
                        .Build());
                });
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateSupplier([FromBody] CreateUpdateSupplierRequest supplier)
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

        [HttpDelete("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteSupplierById(Guid Id)
        {
            var result = await sender.Send(new DeleteSupplierByIdCommand(Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Supplier was successfully deleted.")
                    .Build()),
                f =>
                {
                    if (f is NotFoundSupplierException ex)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(ex.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddResultMessage(f.Message)
                        .Build());
                });
        }

        [HttpPut("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> UpdateSupplier(Guid Id, [FromBody] CreateUpdateSupplierRequest supplier)
        {
            var result = await sender.Send(new UpdateSupplierCommand(Id, supplier));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .AddResultMessage("Supplier was successfully updated.")
                    .Build()),
                f =>
                {
                    if (f is NotFoundSupplierException ex)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(ex.Message)
                            .Build());

                    if (f is FailedValidationException validationException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(validationException.Message)
                            .AddErrors(validationException.Errors.ToList())
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddResultMessage(f.Message)
                        .Build());
                });
        }
    }
}
