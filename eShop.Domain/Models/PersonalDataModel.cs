namespace eShop.Domain.Models
{
    public class PersonalDataModel
    {
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = new DateTime(2000, 01, 01);
    }
}
