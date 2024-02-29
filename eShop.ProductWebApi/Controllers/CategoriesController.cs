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
                f => BadRequest(new ResponseBuilder()
                    .Failed()
                    .AddErrorMessage(f.Message)
                    .Build()));
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
                f => BadRequest(new ResponseBuilder()
                    .Failed()
                    .AddErrorMessage(f.Message)
                    .Build()));
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
                f => BadRequest(new ResponseBuilder()
                    .Failed()
                    .AddErrorMessage(f.Message)
                    .Build()));
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateCategory([FromBody] CategoryDto category)
        {
            var result = await sender.Send(new CreateProductCategoryCommand(category));

            return result.Match<ActionResult<ResponseDto>>(
                s => CreatedAtAction("GetCategoryById", new { Id = s.CategoryId }, new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Category was successfully created.")
                    .AddResult(s)
                    .Build()),
                f =>
                {
                    if (f is FailedValidationException exception)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(exception.Message)
                            .AddErrors(exception.Errors.ToList())
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
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
                f =>
                {
                    if (f is NotFoundCategoryException exception)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(exception.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
        }

        [HttpPut("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> UpdateCategoryById(Guid Id, [FromBody] CategoryDto Category)
        {
            var result = await sender.Send(new UpdateCategoryCommand(Category, Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResult(s)
                    .AddResultMessage("Category was successfully updated.")
                    .Build()),
                f =>
                {
                    if (f is NotFoundCategoryException exception)
                        return new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(exception.Message)
                            .Build();

                    if (f is FailedValidationException validation)
                        return new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(validation.Message)
                            .AddErrors(validation.Errors.ToList())
                            .Build();

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
        }
    }
}
