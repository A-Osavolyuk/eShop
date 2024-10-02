using eShop.Application.Utilities;
using eShop.Domain.Requests.Brand;
using eShop.ProductWebApi.Commands.Brands;
using eShop.ProductWebApi.Queries.Brands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class BrandsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet, AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetBrandsList()
        {
            var result = await sender.Send(new GetBrandsListQuery());
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("{Id:guid}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetBrandById(Guid Id)
        {
            var result = await sender.Send(new GetBrandByIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("{Name}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetBrandById(string Name)
        {
            var result = await sender.Send(new GetBrandByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDTO>> CreateBrand([FromBody]CreateBrandRequest request)
        {
            var result = await sender.Send(new CreateBrandCommand(request));

            return result.Match(
                s => StatusCode(201, new ResponseBuilder().Succeeded().WithResultMessage("Successfully created.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete]
        public async ValueTask<ActionResult<ResponseDTO>> DeleteBrand([FromBody] DeleteBrandRequest request)
        {
            var result = await sender.Send(new DeleteBrandCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage("Successfully deleted.").Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPut]
        public async ValueTask<ActionResult<ResponseDTO>> UpdateBrand([FromBody] UpdateBrandRequest UpdateBrandRequest)
        {
            var result = await sender.Send(new UpdateBrandCommand(UpdateBrandRequest));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage("Successfully updated.").Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
