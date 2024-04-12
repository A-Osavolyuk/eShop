using eShop.Application.Utilities;
using eShop.ProductWebApi.Queries.Brands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BrandsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet("get-brands-list")]
        public async ValueTask<ActionResult<ResponseDTO>> GetBrandsList()
        {
            var result = await sender.Send(new GetBrandsListQuery());
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().AddResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-brand-by-id/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetBrandById(Guid Id)
        {
            var result = await sender.Send(new GetBrandByIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().AddResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-brand-by-name/{Name}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetBrandById(string Name)
        {
            var result = await sender.Send(new GetBrandByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().AddResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
