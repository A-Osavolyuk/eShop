using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.DTOs.Responses.Auth;
using PersonalData = eShop.Domain.Models.PersonalData;

namespace eShop.Application.MappingProfiles
{
    public class PersonalDataProfile : Profile
    {
        public PersonalDataProfile()
        {
            CreateMap<PersonalDataDto, ChangePersonalDataRequest>();
            CreateMap<PersonalDataResponse, PersonalDataDto>();
            CreateMap<Domain.Entities.Admin.PersonalData, PersonalDataResponse>();
            CreateMap<Domain.Entities.Admin.PersonalData, ChangeEmailResponse>();
            CreateMap<PersonalData, ChangePersonalDataRequest>();
            CreateMap<ChangePersonalDataResponse, PersonalData>();
            CreateMap<ChangePersonalDataRequest, Domain.Entities.Admin.PersonalData?>();
            CreateMap<Domain.Entities.Admin.PersonalData?, ChangePersonalDataResponse>();
        }
    }
}
