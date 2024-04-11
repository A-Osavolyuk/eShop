using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;

namespace eShop.Application.MappingProfiles
{
    public class PersonalDataProfile : Profile
    {
        public PersonalDataProfile()
        {
            CreateMap<PersonalDataDTO, ChangePersonalDataRequest>();
            CreateMap<PersonalDataResponse, PersonalDataDTO>();
        }
    }
}
