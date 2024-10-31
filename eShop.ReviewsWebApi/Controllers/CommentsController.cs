namespace eShop.ReviewsWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    public class CommentsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;
        
    }
}
