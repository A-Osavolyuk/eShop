using eShop.Application.Utilities;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet("get-all-products-list")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductList()
        {
            var result = await sender.Send(new GetProductsListQuery());
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().AddResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-all-products-list-by-type/{type}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductListByType(ProductType type)
        {
            var result = await sender.Send(new GetProductsListByTypeQuery(type));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().AddResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
