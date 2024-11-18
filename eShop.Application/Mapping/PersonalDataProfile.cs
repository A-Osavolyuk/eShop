using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Requests.Auth;
using eShop.Domain.Responses.Auth;
using PersonalData = eShop.Domain.Models.PersonalData;

namespace eShop.Application.Mapping
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
