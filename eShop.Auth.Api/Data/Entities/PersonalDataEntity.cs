namespace eShop.Auth.Api.Data.Entities;

public record class PersonalDataEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = new DateTime(1980, 1, 1);

    [JsonIgnore] public string UserId { get; set; } = string.Empty;
    [JsonIgnore] public AppUser? User { get; set; }
}