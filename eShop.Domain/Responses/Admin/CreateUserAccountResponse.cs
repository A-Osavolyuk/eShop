using eShop.Domain.Entities.Admin;

namespace eShop.Domain.Responses.Admin
{
    public class CreateUserAccountResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
