using Microsoft.AspNetCore.Mvc;

namespace eShop.Auth.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class ProfileController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;
    
    [Authorize(Policy = "ManageAccountPolicy")]
    [HttpGet("get-personal-data/{email}")]
    public async ValueTask<ActionResult<Response>> GetPersonalData(string email)
    {
        var result = await sender.Send(new GetPersonalDataQuery(email));

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
    
    [Authorize(Policy = "ManageAccountPolicy")]
    [HttpGet("get-phone-number/{email}")]
    public async ValueTask<ActionResult<Response>> GetPhoneNumber(string email)
    {
        var result = await sender.Send(new GetPhoneNumberQuery(email));

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
    
    [Authorize(Policy = "ManageAccountPolicy")]
    [HttpPut("change-user-name")]
    [ValidationFilter]
    public async ValueTask<ActionResult<Response>> ChangeUserName(
        [FromBody] ChangeUserNameRequest changeUserNameRequest)
    {
        var result = await sender.Send(new ChangeUserNameCommand(changeUserNameRequest));

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
    
    [Authorize(Policy = "ManageAccountPolicy")]
    [HttpPut("change-personal-data")]
    [ValidationFilter]
    public async ValueTask<ActionResult<Response>> ChangePersonalData(
        [FromBody] ChangePersonalDataRequest changePersonalDataRequest)
    {
        var result = await sender.Send(new ChangePersonalDataCommand(changePersonalDataRequest));

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage("Personal data was successfully changed.")
                .WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }
}