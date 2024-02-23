using eShop.ProductWebApi.Categories.Get;

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

        [HttpGet("getByName/{Name:alpha}")]
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
    }
}
