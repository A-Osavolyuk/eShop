using eShop.Application.Utilities;
using eShop.AuthWebApi.Commands.Admin;

namespace eShop.AuthWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class AdminController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [HttpPost("assign-role")]
        public async ValueTask<ActionResult<ResponseDTO>> AssignRoleAsync([FromBody] AssignRoleRequest request)
        {
            var result = await sender.Send(new AssignRoleCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [HttpPost("remove-user-roles")]
        public async ValueTask<ActionResult<ResponseDTO>> RemoveUserRolesAsync([FromBody] RemoveUserRolesRequest request)
        {
            var result = await sender.Send(new RemoveUserRolesCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
