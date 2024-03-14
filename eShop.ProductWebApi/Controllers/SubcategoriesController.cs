using eShop.Domain.Exceptions.Categories;
using eShop.Domain.Exceptions.Subcategories;

namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SubcategoriesController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet]
        public async ValueTask<ActionResult<ResponseDto>> GetSubcategoriesList()
        {
            var result = await sender.Send(new GetSubcategoriesListQuery());

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                            .Succeeded()
                            .AddResult(s)
                            .Build()),
                f => StatusCode(500, new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(f.Message)
                            .Build()));
        }

        [HttpGet("getById/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> GetSubcategoryById(Guid Id)
        {
            var result = await sender.Send(new GetSubcategoryByIdQuery(Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                        .Succeeded()
                        .AddResult(s)
                        .Build()),
                f =>
                {
                    if (f is NotFoundSubcategoryException ex)
                        return NotFound(new ResponseBuilder()
                                .Failed()
                                .AddErrorMessage(ex.Message)
                                .Build());

                    return StatusCode(500, new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(f.Message)
                            .Build());
                });
        }

        [HttpGet("getByName/{Name}")]
        public async ValueTask<ActionResult<ResponseDto>> GetSubcategoryByName(string Name)
        {
            var result = await sender.Send(new GetSubcategoryByNameQuery(Name));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                        .Succeeded()
                        .AddResult(s)
                        .Build()),
                f =>
                {
                    if (f is NotFoundSubcategoryException ex)
                        return NotFound(new ResponseBuilder()
                                .Failed()
                                .AddErrorMessage(ex.Message)
                                .Build());

                    return StatusCode(500, new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(f.Message)
                            .Build());
                });
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateSubcategory([FromBody] CreateUpdateSubcategoryRequestDto Subcategory)
        {
            var result = await sender.Send(new CreateSubcategoryCommand(Subcategory));

            return result.Match<ActionResult<ResponseDto>>(
                s => CreatedAtAction(nameof(GetSubcategoryById), new { Id = s.SubcategoryId } ,new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Subcategory was successfully created.")
                    .AddResult(s)
                    .Build()),
                f =>
                {
                    if (f is NotFoundSubcategoryException || f is NotFoundCategoryException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(f.Message)
                            .Build());

                    if (f is FailedValidationException ex)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(ex.Message)
                            .AddErrors(ex.Errors.ToList())
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
        }

        [HttpDelete("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteSubcategoryById(Guid Id)
        {
            var result = await sender.Send(new DeleteSubcategoryByIdCommand(Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Subcategory was successfully deleted.")
                    .Build()),
                f =>
                {
                    if (f is NotFoundSubcategoryException ex)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(ex.Message)
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
        }

        [HttpPut("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> UpdateSubcategory(Guid Id, [FromBody] CreateUpdateSubcategoryRequestDto Subcategory)
        {
            var result = await sender.Send(new UpdateSubcategoryCommand(Subcategory, Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Subcategory was successfully updated.")
                    .AddResult(s)
                    .Build()),
                f =>
                {
                    if (f is NotFoundSubcategoryException || f is NotFoundCategoryException)
                        return NotFound(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(f.Message)
                            .Build());

                    if (f is FailedValidationException ex)
                        return BadRequest(new ResponseBuilder()
                            .Failed()
                            .AddErrorMessage(ex.Message)
                            .AddErrors(ex.Errors.ToList())
                            .Build());

                    return StatusCode(500, new ResponseBuilder()
                        .Failed()
                        .AddErrorMessage(f.Message)
                        .Build());
                });
        }
    }
}
