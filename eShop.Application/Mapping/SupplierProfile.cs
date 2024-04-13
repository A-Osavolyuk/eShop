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
            CreateMap<Supplier, SupplierDTO>();
            CreateMap<CreateSupplierRequest, Supplier>();
            CreateMap<UpdateSupplierRequest, Supplier>();
        }
    }
}
