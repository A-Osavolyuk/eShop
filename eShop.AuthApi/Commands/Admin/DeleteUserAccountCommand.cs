using eShop.Domain.Requests.AuthApi.Admin;
using eShop.Domain.Responses.AuthApi.Admin;

namespace eShop.AuthApi.Commands.Admin;

internal sealed record DeleteUserAccountCommand(DeleteUserAccountRequest Request)
    : IRequest<Result<DeleteUserAccountResponse>>;

internal sealed class DeleteUserAccountCommandHandler(
    AppManager appManager,
    AuthDbContext context) : IRequestHandler<DeleteUserAccountCommand, Result<DeleteUserAccountResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly AuthDbContext context = context;

    public async Task<Result<DeleteUserAccountResponse>> Handle(DeleteUserAccountCommand request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByIdAsync(request.Request.UserId);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with ID {request.Request.UserId}"));
        }

        var rolesResult = await appManager.UserManager.RemoveFromRolesAsync(user);

        if (!rolesResult.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot remove roles from user with ID {request.Request.UserId} " +
                $"due to server error: {rolesResult.Errors.First().Description}"));
        }

        var permissionsResult = await appManager.PermissionManager.RemoveUserFromPermissionsAsync(user);

        if (!permissionsResult.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot remove permissions from user with ID {request.Request.UserId} " +
                $"due to server error: {permissionsResult.Errors.First().Description}"));
        }

        var personalData =
            await context.PersonalData.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == user.Id,
                cancellationToken: cancellationToken);

        if (personalData is not null)
        {
            context.PersonalData.Remove(personalData);
            await context.SaveChangesAsync(cancellationToken);
        }

        var userTokens = await context.UserAuthenticationTokens.AsNoTracking()
            .SingleOrDefaultAsync(x => x.UserId == user.Id, cancellationToken: cancellationToken);

        if (userTokens is not null)
        {
            context.UserAuthenticationTokens.Remove(userTokens);
            await context.SaveChangesAsync(cancellationToken);
        }

        var accountResult = await appManager.UserManager.DeleteAsync(user);

        if (!accountResult.Succeeded)
        {
            return new(new FailedOperationException(
                $"Cannot delete user account with ID {request.Request.UserId} " +
                $"due to server error: {accountResult.Errors.First().Description}"));
        }

        return new(
            new DeleteUserAccountResponse()
            {
                Message = "User account was successfully deleted.", Succeeded = true
            });
    }
}