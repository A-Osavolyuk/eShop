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
                s => Ok(new ResponseBuilder().Succeeded().AddResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
