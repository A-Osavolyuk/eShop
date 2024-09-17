namespace eShop.AuthWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class AdminController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpGet("find-user-by-email/{Email}")]
        public async ValueTask<ActionResult<ResponseDTO>> FindUserByEmailAsync(string Email)
        {
            var result = await sender.Send(new FindUserByEmailQuery(Email));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("find-user-by-id/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDTO>> FindUserByIdAsync(Guid Id)
        {
            var result = await sender.Send(new FindUserByIdQuery(Id));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpGet("get-roles")]
        public async ValueTask<ActionResult<ResponseDTO>> GetRolesListAsync()
        {
            var result = await sender.Send(new GetRolesListQuery());

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("assign-role")]
        public async ValueTask<ActionResult<ResponseDTO>> AssignRoleAsync([FromBody] AssignRoleRequest request)
        {
            var result = await sender.Send(new AssignRoleCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("create-role")]
        public async ValueTask<ActionResult<ResponseDTO>> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            var result = await sender.Send(new CreateRoleCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete("remove-user-roles")]
        public async ValueTask<ActionResult<ResponseDTO>> RemoveUserRolesAsync([FromBody] RemoveUserRolesRequest request)
        {
            var result = await sender.Send(new RemoveUserRolesCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete("remove-user-role")]
        public async ValueTask<ActionResult<ResponseDTO>> RemoveUserRoleAsync([FromBody] RemoveUserRoleRequest request)
        {
            var result = await sender.Send(new RemoveUserRoleCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpDelete("delete-role")]
        public async ValueTask<ActionResult<ResponseDTO>> DeleteRoleAsync([FromBody] DeleteRoleRequest request)
        {
            var result = await sender.Send(new DeleteRoleCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
