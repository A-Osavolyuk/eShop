using eShop.Domain.Messages.Sms;
using eShop.Domain.Models;

namespace eShop.AuthApi.Commands.Auth;

internal sealed record ChangePhoneNumberCommand(ChangePhoneNumberRequest Request) : IRequest<Result<ChangePhoneNumberResponse>>;

internal sealed class RequestChangePhoneNumberCommandHandler(
    AppManager appManager,
    ISmsService smsService,
    IConfiguration configuration) : IRequestHandler<ChangePhoneNumberCommand, Result<ChangePhoneNumberResponse>>
{
    private readonly AppManager appManager = appManager;
    private readonly ISmsService smsService = smsService;
    private readonly IConfiguration configuration = configuration;
    private readonly string frontendUri = configuration["Configuration:General:Frontend:Clients:BlazorServer:Uri"]!;

    public async Task<Result<ChangePhoneNumberResponse>> Handle(ChangePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var user = await appManager.UserManager.FindByPhoneNumberAsync(request.Request.CurrentPhoneNumber);

        if (user is null)
        {
            return new(new NotFoundException($"Cannot find user with phone number {request.Request.CurrentPhoneNumber}."));
        }

        var destinationSet = new DestinationSet()
        {
            Current = user.PhoneNumber!,
            Next = request.Request.NewPhoneNumber
        };
        var code = await appManager.SecurityManager.GenerateVerificationCodeSetAsync(destinationSet, CodeType.ChangePhoneNumber);

        await smsService.SendMessageAsync("phone-number-change", new ChangePhoneNumberMessage()
        {
            Code = code.Current,
            PhoneNumber = request.Request.NewPhoneNumber
        });
        
        await smsService.SendMessageAsync("new-phone-number-verification", new ChangePhoneNumberMessage()
        {
            Code = code.Next,
            PhoneNumber = request.Request.NewPhoneNumber
        });

        return new(new ChangePhoneNumberResponse()
        {
            Message = "We have sent sms messages to your phone numbers."
        });
    }
}