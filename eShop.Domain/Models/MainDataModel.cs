namespace eShop.Domain.Models
{
    public class MainDataModel
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = new DateTime(2000, 01, 01);
    }
}
