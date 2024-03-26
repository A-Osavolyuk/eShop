namespace eShop.Domain.DTOs
{
    public class PersonalData
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public string Gender { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = new DateTime(1980, 1, 1);
    }
}
