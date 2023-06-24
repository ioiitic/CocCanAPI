﻿using CocCanService.DTOs.Menu;
using CocCanService.DTOs.MenuDetail;
using CocCanService.DTOs.OrderDetail;
using CocCanService.Services.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services
{
    public interface IMenuDetailService 
    {
        Task<ServiceResponse<List<DTOs.MenuDetail.MenuDetailDTO>>> GetAllMenuDetailsAsync();
        Task<ServiceResponse<List<CreateMenuDetailDTO>>> CreateMenuDetailAsync(Guid menuID, List<CreateMenuDetailDTO> createMenuDetailDTOList);
        Task<ServiceResponse<DTOs.MenuDetail.MenuDetailDTO>> UpdateMenuDetailAsync(MenuDetailDTO menuDetailDTO);
        Task<ServiceResponse<DTOs.MenuDetail.MenuDetailDTO>> GetMenuDetailByIdAsync(Guid id);
        Task<ServiceResponse<string>> HardDeleteMenuDetailAsync(Guid id);
    }
}
