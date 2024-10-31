namespace eShop.ReviewsWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ReviewsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;
    }
}
