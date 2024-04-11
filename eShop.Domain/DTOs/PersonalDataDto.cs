namespace eShop.Domain.DTOs
{
    public class PersonalDataDTO
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Gender { get; set; } = "";
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = new DateTime(1980, 1, 1);
    }
}
