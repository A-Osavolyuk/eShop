using eShop.Domain.Requests.Auth;
using eShop.Domain.Responses.Auth;

namespace eShop.AuthApi.Receivers
{
    public class UserExistsReceiver(UserManager<AppUser> userManager) : IConsumer<UserExistsRequest>
    {
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task Consume(ConsumeContext<UserExistsRequest> context)
        {
            var result = await userManager.FindByIdAsync(context.Message.UserId.ToString());

            if (result is null)
            {
                await context.RespondAsync(new UserExistsResponse() { Succeeded = false, Message = $"Cannot find user" });
            }
            else
            {
                await context.RespondAsync(new UserExistsResponse() { Succeeded = true, Message = $"User already exists" });
            }
        }
    }
}
