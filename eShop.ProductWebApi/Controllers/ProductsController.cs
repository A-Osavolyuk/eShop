using eShop.Application.Utilities;
using eShop.Domain.Enums;
using eShop.Domain.Requests.Product;
using eShop.ProductWebApi.Commands;
using LanguageExt.ClassInstances;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using ProductEntity = eShop.Domain.Entities.Product.ProductEntity;
using ShoesEntity = eShop.Domain.Entities.Product.ShoesEntity;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductsController(ISender sender, IMongoDatabase database) : ControllerBase
    {
        private readonly ISender sender = sender;
        private readonly IMongoDatabase database = database;

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
