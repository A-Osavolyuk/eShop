namespace eShop.Domain.DTOs.Requests
{
    public class ChangePersonalDataRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = new DateTime(1980, 1, 1);
    }
}
