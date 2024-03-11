using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;

namespace eShop.Application.MappingProfiles
{
    public class PersonalDataProfile : Profile
    {
        public PersonalDataProfile()
        {
            CreateMap<PersonalDataDto, ChangePersonalDataRequestDto>();
        }
    }
}
