namespace eShop.Domain.Types;

public record PersonalData
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; } = new DateTime(1980, 1, 1);
}