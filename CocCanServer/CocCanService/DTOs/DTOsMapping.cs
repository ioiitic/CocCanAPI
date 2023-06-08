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
using CocCanService.DTOs.Order;
using CocCanService.DTOs.OrderDetail;
using CocCanService.DTOs.MenuDetail;

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
            CreateMap<Repository.Entities.Product, ProductDTO>();
            CreateMap<CreateProductDTO, Repository.Entities.Product>()
                .ForMember(
                    des => des.Id,
                    act => Guid.NewGuid())
                .ForMember(
                    des => des.Status,
                    act => act.UseValue(1));
            CreateMap<Repository.Entities.Store, StoreDTO>().ReverseMap();
            CreateMap<Repository.Entities.Location, LocationDTO>().ReverseMap();
            CreateMap<Repository.Entities.Category, CategoryDTO>().ReverseMap();
            CreateMap<Repository.Entities.Menu, MenuDTO>().ReverseMap();
            CreateMap<Repository.Entities.Order, OrderDTO>().ReverseMap();
            CreateMap<Repository.Entities.OrderDetail, OrderDetailDTO>().ReverseMap();
            CreateMap<Repository.Entities.MenuDetail, MenuDetailDTO>().ReverseMap();
        }
    }
}
