﻿using AutoMapper;
using eShop.Domain.DTOs.Requests;
using eShop.Domain.DTOs.Responses;
using eShop.Domain.Entities;

namespace eShop.Application.MappingProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierDto, SupplierEntity>();
            CreateMap<SupplierEntity, SupplierDto>();
            CreateMap<CreateUpdateSupplierRequestDto, SupplierDto>();
        }
    }
}