using eShop.Application.Utilities;
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

        [HttpGet("get-suppliers-list")]
        public async ValueTask<ActionResult<ResponseDTO>> GetSuppliersList()
        {
            var result = await sender.Send(new GetSuppliersListQuery());
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-supplier-by-id/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetSupplierById(Guid Id)
        {
            var result = await sender.Send(new GetBrandByIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-supplier-by-name/{Name}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetSupplierById(string Name)
        {
            var result = await sender.Send(new GetBrandByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
