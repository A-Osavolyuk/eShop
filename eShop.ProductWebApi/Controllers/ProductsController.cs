using eShop.Application.Utilities;
using eShop.Domain.Requests.Product;
using eShop.ProductWebApi.Commands.Products;
using eShop.ProductWebApi.Queries.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProductsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet, AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductList()
        {
            var result = await sender.Send(new GetProductsListQuery());
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-products-with-name/{Name}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductsByName(string Name)
        {
            var result = await sender.Send(new GetProductsByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-products-with-variantId/{VariantId:guid}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductsByVariantId(Guid VariantId)
        {
            var result = await sender.Send(new GetProductsByVariantIdQuery(VariantId));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("{Id:guid}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductById(Guid Id)
        {
            var result = await sender.Send(new GetProductByIdQuery(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("{Article:long}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductByArticle(long Article)
        {
            var result = await sender.Send(new GetProductByArticleQuery(Article));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("{Name}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> GetProductByName(string Name)
        {
            var result = await sender.Send(new GetProductByNameQuery(Name));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("search-by-article/{Article:long}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> SearchByArticle(long Article)
        {
            var result = await sender.Send(new SearchProductByArticleQuery(Article));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("search-by-name/{Name}"), AllowAnonymous]
        public async ValueTask<ActionResult<ResponseDTO>> SearchByName(string Name)
        {
            var result = await sender.Send(new SearchProductByNameQuery(Name));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDTO>> CreateProduct([FromBody] IEnumerable<CreateProductRequest> requests)
        {
            var result = await sender.Send(new CreateProductCommand(requests));
            return result.Match(
                s => StatusCode(201, new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage("Products were successfully created.").Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPut]
        public async ValueTask<ActionResult<ResponseDTO>> UpdateProduct([FromBody] UpdateProductRequest updateProductRequest)
        {
            var result = await sender.Send(new UpdateProductCommand(updateProductRequest));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage("Product was successfully updated.").WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> DeleteProductById(Guid Id)
        {
            var result = await sender.Send(new DeleteProductByIdCommand(Id));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage("Product was successfully deleted.").Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
