using eShop.Domain.DTOs.Requests.Cart;
using eShop.Domain.DTOs.Responses.Cart;
using LanguageExt;

namespace eShop.AuthWebApi.Receivers
{
    public class UserExistsReceiver(UserManager<AppUser> userManager) : IConsumer<UserExistsRequest>
    {
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task Consume(ConsumeContext<UserExistsRequest> context)
        {
            var result = (await userManager.FindByIdAsync(context.Message.UserId.ToString())).IsNull();

            await context.RespondAsync(new UserExistsResponse() { Exists = !result });
        }
    }
}
