using eShop.Application.Utilities;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Enums;
using eShop.ProductWebApi.Commands.Products;
using eShop.ProductWebApi.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet("get-products-list")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductList()
        {
            var result = await sender.Send(new GetProductsListQuery());
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-products-with-name/{Name}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductsByName(string Name)
        {
            var result = await sender.Send(new GetProductsByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-by-id/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductById(Guid Id)
        {
            var result = await sender.Send(new GetProductByIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-by-article/{Article:long}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductByArticle(long Article)
        {
            var result = await sender.Send(new GetProductByArticleQuery(Article));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-by-name/{Name}")]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductByName(string Name)
        {
            var result = await sender.Send(new GetProductByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("create-product-clothing")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateClothingProduct([FromBody] CreateClothingRequest createProductRequest)
        {
            var result = await sender.Send(new CreateClothingProductCommand(createProductRequest));
            return result.Match(
                s => CreatedAtAction(nameof(GetProductById), new { s.Id }, 
                    new ResponseBuilder().Succeeded().WithResultMessage("Product was successfully created.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("create-products-clothing")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateClothingProducts([FromBody] CreateClothingRequest createProductRequest)
        {
            var result = await sender.Send(new CreateClothingProductsCommand(createProductRequest));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage("Product was successfully created.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("create-product-shoes")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateShoesProduct([FromBody] CreateShoesRequest createProductRequest)
        {
            var result = await sender.Send(new CreateShoesProductCommand(createProductRequest));
            return result.Match(
                s => CreatedAtAction(nameof(GetProductById), new { s.Id },
                    new ResponseBuilder().Succeeded().WithResultMessage("Product was successfully created.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPut("update-product-clothing/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> UpdateClothingProduct([FromBody] UpdateClothingRequest updateProductRequest, Guid Id)
        {
            var result = await sender.Send(new UpdateClothingProductCommand(updateProductRequest, Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage("Product was successfully updated.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPut("update-product-shoes/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> UpdateShoesProduct([FromBody] UpdateShoesRequest updateProductRequest, Guid Id)
        {
            var result = await sender.Send(new UpdateShoesProductCommand(updateProductRequest, Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage("Product was successfully updated.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete("delete-by-id/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> DeleteProductById(Guid Id)
        {
            var result = await sender.Send(new DeleteProductByIdCommand(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage("Product was successfully deleted.").Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("search-by-article/{Article:long}")]
        public async ValueTask<ActionResult<ResponseDTO>> SearchByArticle(long Article)
        {
            var result = await sender.Send(new SearchProductByArticleQuery(Article));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("search-by-name/{Name}")]
        public async ValueTask<ActionResult<ResponseDTO>> SearchByName(string Name)
        {
            var result = await sender.Send(new SearchProductByNameQuery(Name));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
