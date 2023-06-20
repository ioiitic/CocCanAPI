using AutoMapper;
using CocCanService.DTOs.Product;
using CocCanService.DTOs.Category;
using CocCanService.DTOs.Location;
using CocCanService.DTOs.Menu;
using CocCanService.DTOs.Staff;
using CocCanService.DTOs.Store;
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
            //Staff
            //CreateMap<Repository.Entities.Staff, StaffDTO>()
            //    .ForMember(
            //        des => des.Role,
            //        act => act.MapFrom(src => (Enum.RoleType)src.Role));
            //CreateMap<CreateStaffDTO, Repository.Entities.Staff>()
            //    .ForMember(
            //        des => des.Id,
            //        act => Guid.NewGuid())
            //    .ForMember(
            //        des => des.Role,
            //        act => act.UseValue(0))
            //    .ForMember(
            //        des => des.Status,
            //        act => act.UseValue(1));
            //Product
            CreateMap<Repository.Entities.Product, ProductDTO>().ReverseMap();
            CreateMap<CreateProductDTO, Repository.Entities.Product>()
                .ForMember(
                    des => des.Id,
                    act => Guid.NewGuid())
                .ForMember(
                    des => des.Status,
                    act => act.UseValue(1));
            //Store
            CreateMap<Repository.Entities.Store, StoreDTO>().ReverseMap()
                .ForMember(
                    des => des.Products,
                    act => act.MapFrom(src => src.Products));
            CreateMap<CreateStoreDTO, Repository.Entities.Store>()
                .ForMember(
                    des => des.Id,
                    act => Guid.NewGuid())
                .ForMember(
                    des => des.Status,
                    act => act.UseValue(1));
            //CreateMap<UpdateStoreDTO, Repository.Entities.Store>()
            //    .ForMember(
            //        des => des.Name,
            //        act =>
            //            {
            //                act.UseDestinationValue();
            //                act.PreCondition(src => src.Name != "");
            //                act.MapFrom(src => src.Name);
            //            })
            //    .ForMember(
            //        des => des.Image,
            //        act =>
            //        {
            //            act.UseDestinationValue();
            //            act.PreCondition(src => src.Image != "");
            //            act.MapFrom(src => src.Image);
            //        })
            //    .ForMember(
            //        des => des.Id,
            //        act =>
            //            {
            //                act.UseDestinationValue();
            //                act.Ignore();
            //            })
            //    .ForMember(
            //        des => des.Status,
            //        act =>
            //        {
            //            act.UseDestinationValue();
            //            act.Ignore();
            //        });
            //Location
            //CreateMap<Repository.Entities.Location, LocationDTO>().ReverseMap();
            //Category
            //CreateMap<Repository.Entities.Category, CategoryDTO>().ReverseMap();
            //CreateMap<CreateProductDTO, Repository.Entities.Product>()
            //    .ForMember(
            //        des => des.Id,
            //        act => Guid.NewGuid())
            //    .ForMember(
            //        des => des.Status,
            //        act => act.UseValue(1));
            //Menu
            //CreateMap<Repository.Entities.Menu, MenuDTO>().ReverseMap();
        }
    }
}
