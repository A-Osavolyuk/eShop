using eShop.Domain.Entities.AuthApi;
using eShop.Domain.Requests.AuthApi.Account;
using eShop.Domain.Requests.AuthApi.Auth;
using eShop.Domain.Responses.AuthApi.Auth;

namespace eShop.Application.Mapping;

public static class PersonalDataMapper
{
    public static PersonalDataResponse ToPersonalDataResponse(PersonalDataEntity entity)
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

    public static ChangePersonalDataRequest ToChangePersonalDataRequest(PersonalDataModel model)
    {
        return new ChangePersonalDataRequest()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Gender = model.Gender,
            DateOfBirth = model.DateOfBirth
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
}