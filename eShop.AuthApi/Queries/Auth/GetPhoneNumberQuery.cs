using eShop.Domain.Responses.AuthApi.Auth;

namespace eShop.AuthApi.Queries.Auth;

internal sealed record GetPhoneNumberQuery(string Email) : IRequest<Result<GetPhoneNumberResponse>>;

internal sealed class GetPhoneNumberQueryHandler(
    AppManager appManager) : IRequestHandler<GetPhoneNumberQuery, Result<GetPhoneNumberResponse>>
{
    private readonly AppManager appManager = appManager;

    public async Task<Result<GetPhoneNumberResponse>> Handle(GetPhoneNumberQuery request,
        CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Email}."));
        }

        return new(new GetPhoneNumberResponse()
        {
            PhoneNumber = user.PhoneNumber!
        });
    }
}