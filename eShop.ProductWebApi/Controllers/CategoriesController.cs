using eShop.Application.Utilities;
using eShop.Domain.DTOs;
using eShop.Domain.Exceptions.Categories;
using eShop.ProductWebApi.Categories.Create;
using eShop.ProductWebApi.Categories.Delete;
using eShop.ProductWebApi.Categories.Get;
using eShop.ProductWebApi.Categories.Update;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoriesController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet]
        public async ValueTask<ActionResult<ResponseDto>> GetAllCategories()
        {
            var result = await sender.Send(new GetCategoriesListQuery());

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("getById/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> GetCategoryById(Guid Id)
        {
            var result = await sender.Send(new GetCategoryByIdQuery(Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("getByName/{Name}")]
        public async ValueTask<ActionResult<ResponseDto>> GetCategoryByName(string Name)
        {
            var result = await sender.Send(new GetCategoryByNameQuery(Name));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateCategory([FromBody] CreateUpdateCategoryRequest Category)
        {
            var result = await sender.Send(new CreateProductCategoryCommand(Category));

            return result.Match<ActionResult<ResponseDto>>(
                s => CreatedAtAction(nameof(GetCategoryById), new { Id = s.CategoryId }, new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Category was successfully created.")
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteCategoryById(Guid Id)
        {
            var result = await sender.Send(new DeleteCategoryByIdCommand(Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage($"Category was successfully deleted.")
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPut("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> UpdateCategoryById(Guid Id, [FromBody] CreateUpdateCategoryRequest Category)
        {
            var result = await sender.Send(new UpdateCategoryCommand(Category, Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .AddResultMessage("Category was successfully updated.")
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
