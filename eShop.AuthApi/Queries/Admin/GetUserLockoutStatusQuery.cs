using eShop.Domain.Responses.AuthApi.Admin;

namespace eShop.AuthApi.Queries.Admin;

internal sealed record GetUserLockoutStatusQuery(string Email) : IRequest<Result<LockoutStatusResponse>>;

internal sealed class GetUserLockoutStatusQueryHandler(
    AppManager appManager) : IRequestHandler<GetUserLockoutStatusQuery, Result<LockoutStatusResponse>>
{
    private readonly AppManager appManager = appManager;

    public async Task<Result<LockoutStatusResponse>> Handle(GetUserLockoutStatusQuery request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Email}."));
        }
            
        var lockoutStatus = await appManager.UserManager.GetLockoutStatusAsync(user);
        return new(UserMapper.ToUserLockoutStatusResponse(lockoutStatus));
    }
}