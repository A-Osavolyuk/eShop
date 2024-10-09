using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.DTOs.Responses.Auth;
using eShop.Domain.Entities.Admin;
using eShop.Domain.Models;

namespace eShop.Application.MappingProfiles
{
    public class PersonalDataProfile : Profile
    {
        public PersonalDataProfile()
        {
            CreateMap<PersonalDataDTO, ChangePersonalDataRequest>();
            CreateMap<PersonalDataResponse, PersonalDataDTO>();
            CreateMap<PersonalData, PersonalDataResponse>();
            CreateMap<PersonalData, ChangeEmailResponse>();
            CreateMap<PersonalDataModel, ChangePersonalDataRequest>();
            CreateMap<ChangePersonalDataResponse, PersonalDataModel>();
            CreateMap<ChangePersonalDataRequest, PersonalData?>();
            CreateMap<PersonalData?, ChangePersonalDataResponse>();
        }
    }
}
