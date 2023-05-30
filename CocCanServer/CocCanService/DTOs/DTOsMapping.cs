﻿using AutoMapper;
using CocCanService.DTOs.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs
{
    public class DTOsMapping : Profile
    {
        public DTOsMapping()
        {
            CreateMap<Repository.Entities.Staff, StaffDTO>()
                .ForMember(
                    des => des.Role,
                    act => act.MapFrom(src => (Enum.RoleType)src.Role));
            CreateMap<CreateStaffDTO, Repository.Entities.Staff>()
                .ForMember(
                    des => des.Id,
                    act => Guid.NewGuid())
                .ForMember(
                    des => des.Role,
                    act => act.UseValue(0))
                .ForMember(
                    des => des.Status,
                    act => act.UseValue(1));
        }
    }
}
