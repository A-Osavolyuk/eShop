namespace eShop.Domain.Responses.Admin
{
    public class RemoveUserRolesResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
