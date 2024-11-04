using eShop.Application.Utilities;
using eShop.Domain.Requests.Product;
using eShop.ProductWebApi.Commands;
using eShop.ProductWebApi.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductsController(ISender sender, IMongoDatabase database) : ControllerBase
    {
        private readonly ISender sender = sender;
        private readonly IMongoDatabase database = database;
        
        [HttpGet("get-products")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateProduct()
        {
            var result = await sender.Send(new GetProductsQuery());

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpPost("create-product")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateProduct([FromBody] CreateProductRequest request)
        {
            var result = await sender.Send(new CreateProductCommand(request));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
    }
}
