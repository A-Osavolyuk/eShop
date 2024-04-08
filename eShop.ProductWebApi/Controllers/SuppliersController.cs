using eShop.Application.Utilities;
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
                f => ExceptionHandler.HandleException(f));
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
                f => ExceptionHandler.HandleException(f));
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
                f => ExceptionHandler.HandleException(f));
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
                f => ExceptionHandler.HandleException(f));
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
                f => ExceptionHandler.HandleException(f));
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
                f => ExceptionHandler.HandleException(f));
        }
    }
}
