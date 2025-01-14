namespace eShop.Auth.Api.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class AdminController(ISender sender) : ControllerBase
{
    private readonly ISender sender = sender;

    [Authorize(Policy = "ManageUsersPolicy")]
    [HttpGet("find-user-by-email/{email}")]
    public async ValueTask<ActionResult<Response>> FindUserByEmailAsync(string email)
    {
        var result = await sender.Send(new FindUserByEmailQuery(email));

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageUsersPolicy")]
    [HttpGet("find-user-by-id/{id:guid}")]
    public async ValueTask<ActionResult<Response>> FindUserByIdAsync(Guid id)
    {
        var result = await sender.Send(new FindUserByIdQuery(id));

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageUsersPolicy")]
    [HttpGet("get-all-users")]
    public async ValueTask<ActionResult<Response>> GetAllUsersAsync()
    {
        var result = await sender.Send(new GetUsersListQuery());

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageRolesPolicy")]
    [HttpGet("get-roles")]
    public async ValueTask<ActionResult<Response>> GetRolesListAsync()
    {
        var result = await sender.Send(new GetRolesListQuery());

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageRolesPolicy")]
    [HttpGet("get-user-roles/{id:guid}")]
    public async ValueTask<ActionResult<Response>> GetUserRolesAsync(Guid id)
    {
        var result = await sender.Send(new GetUserRolesQuery(id));

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageLockoutPolicy")]
    [HttpGet("get-lockout-status/{email}")]
    public async ValueTask<ActionResult<Response>> GetRolesListAsync(string email)
    {
        var result = await sender.Send(new GetUserLockoutStatusQuery(email));

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManagePermissionsPolicy")]
    [HttpGet("get-permissions")]
    public async ValueTask<ActionResult<Response>> GetPermissionsListAsync()
    {
        var result = await sender.Send(new GetPermissionsListQuery());

        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithResult(s).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageRolesPolicy")]
    [HttpPost("assign-role")]
    public async ValueTask<ActionResult<Response>> AssignRoleAsync([FromBody] AssignRoleRequest request)
    {
        var result = await sender.Send(new AssignRoleCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManagePermissionsPolicy")]
    [HttpPost("issue-permissions")]
    public async ValueTask<ActionResult<Response>> IssuePermissionsAsync([FromBody] IssuePermissionRequest request)
    {
        var result = await sender.Send(new IssuePermissionCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageRolesPolicy")]
    [HttpPost("create-role")]
    [ValidationFilter]
    public async ValueTask<ActionResult<Response>> CreateRoleAsync([FromBody] CreateRoleRequest request)
    {
        var result = await sender.Send(new CreateRoleCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageUsersPolicy")]
    [HttpPost("create-user-account")]
    public async ValueTask<ActionResult<Response>> CreateUserAccount([FromBody] CreateUserAccountRequest request)
    {
        var result = await sender.Send(new CreateUserAccountCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageLockoutPolicy")]
    [HttpPost("lockout-user")]
    public async ValueTask<ActionResult<Response>> LockoutUserAsync([FromBody] LockoutUserRequest request)
    {
        var result = await sender.Send(new LockoutUserCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageLockoutPolicy")]
    [HttpPost("unlock-user")]
    public async ValueTask<ActionResult<Response>> UnlockUserAsync([FromBody] UnlockUserRequest request)
    {
        var result = await sender.Send(new UnlockUserCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageRolesPolicy")]
    [HttpDelete("remove-user-roles")]
    public async ValueTask<ActionResult<Response>> RemoveUserRolesAsync([FromBody] RemoveUserRolesRequest request)
    {
        var result = await sender.Send(new RemoveUserRolesCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageRolesPolicy")]
    [HttpDelete("remove-user-role")]
    public async ValueTask<ActionResult<Response>> RemoveUserRoleAsync([FromBody] RemoveUserRoleRequest request)
    {
        var result = await sender.Send(new RemoveUserRoleCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageRolesPolicy")]
    [HttpDelete("delete-role")]
    public async ValueTask<ActionResult<Response>> DeleteRoleAsync([FromBody] DeleteRoleRequest request)
    {
        var result = await sender.Send(new DeleteRoleCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManagePermissionsPolicy")]
    [HttpDelete("delete-user-from-permission")]
    public async ValueTask<ActionResult<Response>> DeleteUserFromPermissionAsync([FromBody] RemoveUserFromPermissionRequest request)
    {
        var result = await sender.Send(new RemoveUserFromPermissionCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }

    [Authorize(Policy = "ManageUsersPolicy")]
    [HttpDelete("delete-user-account")]
    public async ValueTask<ActionResult<Response>> DeleteUserAccountAsync([FromBody] DeleteUserAccountRequest request)
    {
        var result = await sender.Send(new DeleteUserAccountCommand(request));
        return result.Match(
            s => Ok(new ResponseBuilder().Succeeded().WithMessage(s.Message).Build()),
            ExceptionHandler.HandleException);
    }
}