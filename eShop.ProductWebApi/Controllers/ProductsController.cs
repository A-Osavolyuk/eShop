using eShop.Application.Utilities;
using eShop.Domain.DTOs;
using eShop.ProductWebApi.Products.Create;
using eShop.ProductWebApi.Products.Delete;
using eShop.ProductWebApi.Products.Get;
using eShop.ProductWebApi.Products.Update;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet]
        public async ValueTask<ActionResult<ResponseDto>> GetProductsList()
        {
            var result = await sender.Send(new GetProductsListQuery());

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("getById/{Id:Guid}")]
        public async ValueTask<ActionResult<ResponseDto>> GetProductById(Guid Id)
        {
            var result = await sender.Send(new GetProductByIdQuery(Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("getByName/{Name}")]
        public async ValueTask<ActionResult<ResponseDto>> GetProductByName(string Name)
        {
            var result = await sender.Send(new GetProductByNameQuery(Name));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateProduct([FromBody] CreateUpdateProductRequest Product)
        {
            var result = await sender.Send(new CreateProductCommand(Product));

            return result.Match<ActionResult<ResponseDto>>(
                s => CreatedAtAction(nameof(GetProductById), new { Id = s.ProductId }, new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Product was successfully created.")
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete("{Id:Guid}")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteProductById(Guid Id)
        {
            var result = await sender.Send(new DeleteProductByIdCommand(Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Product was successfully deleted.")
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPut("{Id:Guid}")]
        public async ValueTask<ActionResult<ResponseDto>> UpdateProduct(Guid Id, [FromBody] CreateUpdateProductRequest Product)
        {
            var result = await sender.Send(new UpdateProductCommand(Id, Product));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .AddResultMessage("Product was successfully updated.")
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
