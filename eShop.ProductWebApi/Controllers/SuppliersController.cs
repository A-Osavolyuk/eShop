using eShop.Application.Utilities;
using eShop.Domain.DTOs.Requests;
using eShop.ProductWebApi.Commands.Suppliers;
using eShop.ProductWebApi.Queries.Suppliers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SuppliersController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet]
        public async ValueTask<ActionResult<ResponseDTO>> GetSuppliersList()
        {
            var result = await sender.Send(new GetSuppliersListQuery());
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetSupplierById(Guid Id)
        {
            var result = await sender.Send(new GetSupplierByIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("{Name}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetSupplierById(string Name)
        {
            var result = await sender.Send(new GetSupplierByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDTO>> CreateSupplier([FromBody] CreateSupplierRequest body)
        {
            var result = await sender.Send(new CreateSupplierCommand(body));

            return result.Match(
                s => CreatedAtAction(nameof(GetSupplierById), new { s.Id }, new ResponseBuilder().Succeeded().WithResultMessage("Successfully created.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> DeleteSupplierById(Guid Id)
        {
            var result = await sender.Send(new DeleteSupplierCommand(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage("Successfully deleted.").Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPut]
        public async ValueTask<ActionResult<ResponseDTO>> UpdateSupplier([FromBody] UpdateSupplierRequest UpdateSupplierRequest)
        {
            var result = await sender.Send(new UpdateSupplierCommand(UpdateSupplierRequest));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage("Successfully updated.").Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
