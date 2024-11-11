namespace eShop.ProductApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    internal sealed class BrandsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;
    }
}
