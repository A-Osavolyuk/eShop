namespace eShop.ProductWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class BrandsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;
    }
}
