namespace eShop.Domain.Responses.Admin
{
    public class UnlockUserResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
