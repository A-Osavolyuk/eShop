using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.DTOs.Responses.Auth;
using eShop.Domain.Entities.Admin;
using eShop.Domain.Models;
using PersonalData = eShop.Domain.Models.PersonalData;

namespace eShop.Application.MappingProfiles
{
    public class PersonalDataProfile : Profile
    {
        public PersonalDataProfile()
        {
            CreateMap<PersonalDataDTO, ChangePersonalDataRequest>();
            CreateMap<PersonalDataResponse, PersonalDataDTO>();
            CreateMap<Domain.Entities.Admin.PersonalData, PersonalDataResponse>();
            CreateMap<Domain.Entities.Admin.PersonalData, ChangeEmailResponse>();
            CreateMap<PersonalData, ChangePersonalDataRequest>();
            CreateMap<ChangePersonalDataResponse, PersonalData>();
            CreateMap<ChangePersonalDataRequest, Domain.Entities.Admin.PersonalData?>();
            CreateMap<Domain.Entities.Admin.PersonalData?, ChangePersonalDataResponse>();
        }
    }
}
