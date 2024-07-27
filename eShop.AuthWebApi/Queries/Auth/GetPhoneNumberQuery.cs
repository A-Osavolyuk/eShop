
namespace eShop.AuthWebApi.Queries.Auth
{
    public record GetPhoneNumberQuery(string Email) : IRequest<Result<GetPhoneNumberResponse>>;

    public class GetPhoneNumberQueryHandler(IAuthService authService) : IRequestHandler<GetPhoneNumberQuery, Result<GetPhoneNumberResponse>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<GetPhoneNumberResponse>> Handle(GetPhoneNumberQuery request, CancellationToken cancellationToken)
        {
            var result = await authService.GetPhoneNumberAsync(request.Email);
            return result;  
        }
    }
}
