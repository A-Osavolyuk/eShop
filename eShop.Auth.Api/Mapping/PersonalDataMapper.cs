using eShop.Auth.Api.Data.Entities;

namespace eShop.Auth.Api.Mapping;

public static class PersonalDataMapper
{
    public static PersonalDataResponse ToPersonalDataResponse(PersonalData entity)
    {
        return new PersonalDataResponse()
        {
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Gender = entity.Gender,
            DateOfBirth = entity.DateOfBirth
        };
    }

    public static PersonalDataEntity ToPersonalDataEntity(ChangePersonalDataRequest request)
    {
        return new PersonalDataEntity()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth!.Value
        };
    }

    public static PersonalDataEntity ToPersonalDataEntity(SetPersonalDataRequest request)
    {
        return new PersonalDataEntity()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender,
            DateOfBirth = request.BirthDate,
            
        };
    }

    public static PersonalData ToPersonalData(PersonalDataEntity data)
    {
        return new PersonalData()
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            Gender = data.Gender,
            DateOfBirth = data.DateOfBirth,
        };
    }
}