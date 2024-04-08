using eShop.Application.Utilities;
using eShop.Domain.DTOs;
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
                f => ExceptionHandler.HandleException(f));
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
                f => ExceptionHandler.HandleException(f));
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
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResponseDto>> CreateSubcategory([FromBody] CreateUpdateSubcategoryRequest Subcategory)
        {
            var result = await sender.Send(new CreateSubcategoryCommand(Subcategory));

            return result.Match<ActionResult<ResponseDto>>(
                s => CreatedAtAction(nameof(GetSubcategoryById), new { Id = s.SubcategoryId } ,new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Subcategory was successfully created.")
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
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
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPut("{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> UpdateSubcategory(Guid Id, [FromBody] CreateUpdateSubcategoryRequest Subcategory)
        {
            var result = await sender.Send(new UpdateSubcategoryCommand(Subcategory, Id));

            return result.Match<ActionResult<ResponseDto>>(
                s => Ok(new ResponseBuilder()
                    .Succeeded()
                    .AddResultMessage("Subcategory was successfully updated.")
                    .AddResult(s)
                    .Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
