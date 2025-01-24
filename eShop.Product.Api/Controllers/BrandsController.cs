using eShop.Domain.Requests.Api.Brand;
using eShop.Product.Api.Commands.Brands;
using eShop.Product.Api.Queries.Brands;
using Response = eShop.Domain.Common.Api.Response;

namespace eShop.Product.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class BrandsController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;
        
    [HttpGet("get-brands")]
    public async ValueTask<ActionResult<Response>> GetBrandAsync()
    {
        var response = await sender.Send(new GetBrandsQuery());
            
        return response.Match(
            s =>
            {
                var response1 =  new ResponseBuilder().Succeeded().WithResult(s).Build();
                return Ok(response1);
            },
            ExceptionHandler.HandleException);
    }

    [HttpPost("create-brand")]
    [ValidationFilter]
    public async ValueTask<ActionResult<Response>> CreateBrandAsync([FromBody] CreateBrandRequest request)
    {
        var response = await sender.Send(new CreateBrandCommand(request));
            
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
        
    [HttpPut("update-brand")]
    [ValidationFilter]
    public async ValueTask<ActionResult<Response>> UpdateBrandAsync([FromBody] UpdateBrandRequest request)
    {
        var response = await sender.Send(new UpdateBrandCommand(request));
            
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
        
    [HttpDelete("delete-brand")]
    public async ValueTask<ActionResult<Response>> DeleteBrandAsync([FromBody] DeleteBrandRequest request)
    {
        var response = await sender.Send(new DeleteBrandCommand(request));
            
        return response.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
}