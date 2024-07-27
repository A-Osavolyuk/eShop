
namespace eShop.AuthWebApi.Queries.Auth
{
    public record GetPersonalDataQuery(string Email) : IRequest<Result<PersonalDataResponse>>;

    public class GetPersonalDataQueryHandler(IAuthService authService) : IRequestHandler<GetPersonalDataQuery, Result<PersonalDataResponse>>
    {
        private readonly IAuthService authService = authService;

        public async Task<Result<PersonalDataResponse>> Handle(GetPersonalDataQuery request, CancellationToken cancellationToken)
        {
            var result = await authService.GetPersonalDataAsync(request.Email);
            return result;
        }
    }
}
