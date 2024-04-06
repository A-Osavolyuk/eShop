using AutoMapper;
using eShop.Domain.DTOs;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierDto, SupplierEntity>();
            CreateMap<SupplierEntity, SupplierDto>();
            CreateMap<CreateUpdateSupplierRequest, SupplierDto>();
        }
    }
}
