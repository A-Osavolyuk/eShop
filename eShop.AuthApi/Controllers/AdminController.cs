namespace eShop.AuthApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class AdminController(ISender sender) : ControllerBase
    {
        private readonly ISender sender = sender;

        [Authorize(Policy = "ManageUsersPolicy")]
        [HttpGet("find-user-by-email/{Email}")]
        public async ValueTask<ActionResult<ResponseDto>> FindUserByEmailAsync(string Email)
        {
            var result = await sender.Send(new FindUserByEmailQuery(Email));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageUsersPolicy")]
        [HttpGet("find-user-by-id/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> FindUserByIdAsync(Guid Id)
        {
            var result = await sender.Send(new FindUserByIdQuery(Id));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageUsersPolicy")]
        [HttpGet("get-all-users")]
        public async ValueTask<ActionResult<ResponseDto>> GetAllUsersAsync()
        {
            var result = await sender.Send(new GetUsersListQuery());

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageRolesPolicy")]
        [HttpGet("get-roles")]
        public async ValueTask<ActionResult<ResponseDto>> GetRolesListAsync()
        {
            var result = await sender.Send(new GetRolesListQuery());

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageRolesPolicy")]
        [HttpGet("get-user-roles/{Id:guid}")]
        public async ValueTask<ActionResult<ResponseDto>> GetUserRolesAsync(Guid Id)
        {
            var result = await sender.Send(new GetUserRolesQuery(Id));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageLockoutPolicy")]
        [HttpGet("get-lockout-status/{Email}")]
        public async ValueTask<ActionResult<ResponseDto>> GetRolesListAsync(string Email)
        {
            var result = await sender.Send(new GetUserLockoutStatusQuery(Email));

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManagePermissionsPolicy")]
        [HttpGet("get-permissions")]
        public async ValueTask<ActionResult<ResponseDto>> GetPermissionsListAsync()
        {
            var result = await sender.Send(new GetPermissionsListQuery());

            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageRolesPolicy")]
        [HttpPost("assign-role")]
        public async ValueTask<ActionResult<ResponseDto>> AssignRoleAsync([FromBody] AssignRoleRequest request)
        {
            var result = await sender.Send(new AssignRoleCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManagePermissionsPolicy")]
        [HttpPost("issue-permissions")]
        public async ValueTask<ActionResult<ResponseDto>> IssuePermissionsAsync([FromBody] IssuePermissionRequest request)
        {
            var result = await sender.Send(new IssuePermissionCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageRolesPolicy")]
        [HttpPost("create-role")]
        [ValidationFilter]
        public async ValueTask<ActionResult<ResponseDto>> CreateRoleAsync([FromBody] CreateRoleRequest request)
        {
            var result = await sender.Send(new CreateRoleCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageUsersPolicy")]
        [HttpPost("create-user-account")]
        public async ValueTask<ActionResult<ResponseDto>> CreateUserAccount([FromBody] CreateUserAccountRequest request)
        {
            var result = await sender.Send(new CreateUserAccountCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageLockoutPolicy")]
        [HttpPost("lockout-user")]
        public async ValueTask<ActionResult<ResponseDto>> LockoutUserAsync([FromBody] LockoutUserRequest request)
        {
            var result = await sender.Send(new LockoutUserCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageLockoutPolicy")]
        [HttpPost("unlock-user")]
        public async ValueTask<ActionResult<ResponseDto>> UnlockUserAsync([FromBody] UnlockUserRequest request)
        {
            var result = await sender.Send(new UnlockUserCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageRolesPolicy")]
        [HttpDelete("remove-user-roles")]
        public async ValueTask<ActionResult<ResponseDto>> RemoveUserRolesAsync([FromBody] RemoveUserRolesRequest request)
        {
            var result = await sender.Send(new RemoveUserRolesCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageRolesPolicy")]
        [HttpDelete("remove-user-role")]
        public async ValueTask<ActionResult<ResponseDto>> RemoveUserRoleAsync([FromBody] RemoveUserRoleRequest request)
        {
            var result = await sender.Send(new RemoveUserRoleCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageRolesPolicy")]
        [HttpDelete("delete-role")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteRoleAsync([FromBody] DeleteRoleRequest request)
        {
            var result = await sender.Send(new DeleteRoleCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManagePermissionsPolicy")]
        [HttpDelete("delete-user-from-permission")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteUserFromPermissionAsync([FromBody] RemoveUserFromPermissionRequest request)
        {
            var result = await sender.Send(new RemoveUserFromPermissionCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }

        [Authorize(Policy = "ManageUsersPolicy")]
        [HttpDelete("delete-user-account")]
        public async ValueTask<ActionResult<ResponseDto>> DeleteUserAccountAsync([FromBody] DeleteUserAccountRequest request)
        {
            var result = await sender.Send(new DeleteUserAccountCommand(request));
            return result.Match(
                s => Ok(new ResponseBuilder().Succeeded().WithResultMessage(s.Message).Build()),
                f => ExceptionHandler.HandleException(f));
        }
    }
}
