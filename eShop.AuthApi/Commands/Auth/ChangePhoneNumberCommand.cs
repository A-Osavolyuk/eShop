using eShop.Domain.Requests.AuthApi.Auth;
using eShop.Domain.Responses.AuthApi.Auth;

namespace eShop.AuthApi.Commands.Auth;

internal sealed record ChangePhoneNumberCommand(ChangePhoneNumberRequest Request) : IRequest<Result<ChangePhoneNumberResponse>>;

internal sealed class RequestChangePhoneNumberCommandHandler(
    AppManager appManager,
    IEmailSender emailSender,
    IConfiguration configuration) : IRequestHandler<ChangePhoneNumberCommand, Result<ChangePhoneNumberResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly IEmailSender emailSender = emailSender;
    private readonly IConfiguration configuration = configuration;
    private readonly string frontendUri = configuration["GeneralSettings:FrontendBaseUri"]!;

    public async Task<Result<ChangePhoneNumberResponse>> Handle(ChangePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByEmailAsync(request.Request.Email);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with email {request.Request.Email}."));
        }

        var token = await appManager.UserManager.GenerateChangePhoneNumberTokenAsync(user, request.Request.PhoneNumber);

        var link = UrlGenerator.ActionLink("/account/change-phone-number", frontendUri, new
        {
            Token = token,
            Email = request.Request.Email,
            PhoneNumber = request.Request.PhoneNumber
        });

        await emailSender.SendChangePhoneNumberMessage(new ChangePhoneNumberMessage()
        {
            Link = link,
            To = request.Request.Email,
            Subject = "Change phone number request",
            UserName = request.Request.Email,
            PhoneNumber = request.Request.PhoneNumber
        });

        return new(new ChangePhoneNumberResponse()
        {
            Message = "We have sent you an email with instructions."
        });
    }
}