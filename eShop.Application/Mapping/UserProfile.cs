using AutoMapper;
using eShop.Domain.DTOs.Requests.Auth;
using eShop.Domain.Entities;
using eShop.Domain.Entities.Admin;
using eShop.Domain.Responses.Admin;

namespace eShop.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationRequest, AppUser>()
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.NormalizedEmail, opt => opt.MapFrom(src => src.Email.ToUpper()))
                .ForMember(x => x.NormalizedUserName, opt => opt.MapFrom(src => src.Email.ToUpper()));

            CreateMap<AppUser, AccountData>();
            CreateMap<LockoutStatus, UserLockoutStatusResponse>();
        }
    }
}
