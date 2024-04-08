using eShop.Domain.DTOs.Requests;
using Microsoft.AspNetCore.Identity;

namespace eShop.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } = new DateTime(1980, 1, 1);

        public void AddPersonalData(ChangePersonalDataRequest changePersonalDataRequestDto)
        {
            FirstName = changePersonalDataRequestDto.FirstName;
            LastName = changePersonalDataRequestDto.LastName;
            Gender = changePersonalDataRequestDto.Gender;
            DateOfBirth = changePersonalDataRequestDto.DateOfBirth;
        }
    }
}
