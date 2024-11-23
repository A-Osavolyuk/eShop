namespace eShop.ProductApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BrandsController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;
        
        [HttpGet("get-brands")]
        public async ValueTask<ActionResult<ResponseDto>> GetBrandAsync()
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
        public async ValueTask<ActionResult<ResponseDto>> CreateBrandAsync([FromBody] CreateBrandRequest request)
        {
            var response = await sender.Send(new CreateBrandCommand(request));
            
            return response.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpPut("update-brand")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> UpdateBrandAsync([FromBody] UpdateBrandRequest request)
        {
            var response = await sender.Send(new UpdateBrandCommand(request));
            
            return response.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }
        
        [HttpDelete("delete-brand")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteBrandAsync([FromBody] DeleteBrandRequest request)
        {
            var response = await sender.Send(new DeleteBrandCommand(request));
            
            return response.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                ExceptionHandler.HandleException);
        }
    }
}
