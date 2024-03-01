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
    }
}
