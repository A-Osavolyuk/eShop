﻿namespace eShop.ProductApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ProductsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;
        
        [HttpGet("get-products")]
        public async ValueTask<ActionResult<ResponseDto>> GetProductsAsync()
        {
            var result = await sender.Send(new GetProductsQuery());

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpGet("get-product-by-name/{name}")]
        public async ValueTask<ActionResult<ResponseDto>> GetProductByNameAsync(string name)
        {
            var result = await sender.Send(new GetProductByNameQuery(name));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpGet("get-product-by-article/{article}")]
        public async ValueTask<ActionResult<ResponseDto>> GetProductByArticleAsync(string article)
        {
            var result = await sender.Send(new GetProductByArticleQuery(article));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpGet("get-product-by-id/{id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> GetProductByIdAsync(Guid id)
        {
            var result = await sender.Send(new GetProductByIdQuery(id));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpPost("create-product")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> CreateProductAsync([FromBody] CreateProductRequest request)
        {
            var result = await sender.Send(new CreateProductCommand(request));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpPut("update-product")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> UpdateProductAsync([FromBody] UpdateProductRequest request)
        {
            var result = await sender.Send(new UpdateProductCommand(request));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpDelete("delete-product")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteProductAsync([FromBody] DeleteProductRequest request)
        {
            var result = await sender.Send(new DeleteProductCommand(request));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }
    }
}
