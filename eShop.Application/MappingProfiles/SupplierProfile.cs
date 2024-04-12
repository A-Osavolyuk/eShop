using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDTO>();
        }
    }
}
