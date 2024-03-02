using AutoMapper;
using eShop.Domain.DTOs.Requests;

namespace eShop.Application.MappingProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierDto, SupplierProfile>();
        }
    }
}
