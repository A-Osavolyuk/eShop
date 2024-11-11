namespace eShop.ReviewsApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    internal sealed  class ReviewsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;
    }
}
