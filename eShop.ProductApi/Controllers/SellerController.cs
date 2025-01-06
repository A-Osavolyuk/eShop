using Response = eShop.Domain.Common.Api.Response;

namespace eShop.ProductApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class SellerController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;

    [HttpPost("register-seller")]
    public async ValueTask<ActionResult<Response>> RegisterSeller([FromBody] RegisterSellerRequest request)
    {
        var response = await sender.Send(new RegisterSellerCommand(request));
       
        return response.Match(
            s => StatusCode(201, new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
}