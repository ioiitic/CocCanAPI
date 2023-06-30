﻿using AutoMapper;
using CocCanService.DTOs.MenuDetail;
using CocCanService.DTOs.OrderDetail;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class MenuDetailService : IMenuDetailService
    {
        private readonly IMenuDetailRepository _menuDetailRepo;
        private readonly IMapper _mapper;

        public MenuDetailService(IMenuDetailRepository menuDetailRepo, IMapper mapper)
        {
            this._menuDetailRepo = menuDetailRepo;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<MenuDetailDTO>> CreateMenuDetailAsync(CreateMenuDetailDTO createMenuDetailDTO)
        {
            ServiceResponse<MenuDetailDTO> _response = new();
            try
            {
                var _newMenuDetail = _mapper.Map<MenuDetail>(createMenuDetailDTO);

                if (!await _menuDetailRepo.CreateMenuDetailAsync(_newMenuDetail))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Category Repository when trying to create store!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Created";
                _response.Data = _mapper.Map<MenuDetailDTO>(_newMenuDetail);
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }

        public async Task<ServiceResponse<List<MenuDetailDTO>>> GetAllMenuDetailsAsync(string filter)
        {
            //{"session":"d411a66c-0315-4d24-b659-100891ce2628","store":"a6cef7e2-96e5-4110-99a6-05461a4ad5bc"}
            ServiceResponse<List<MenuDetailDTO>> _response = new();
            try
            {
                Dictionary<string, string> _filter = null;

                if (filter != null)
                    _filter = System.Text.Json.JsonSerializer
                        .Deserialize<Dictionary<string, string>>(filter);

                var _MenuDetailList = await _menuDetailRepo.GetAllMenuDetailsAsync(_filter);

                var _MenuDetailListDTO = new List<MenuDetailDTO>();

                foreach (var item in _MenuDetailList)
                {
                    _MenuDetailListDTO.Add(_mapper.Map<MenuDetailDTO>(item));
                }

                _response.Status = true;
                _response.Title = "Created";
                _response.Data = _MenuDetailListDTO;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }

        public async Task<ServiceResponse<MenuDetailDTO>> GetMenuDetailByIdAsync(Guid id)
        {
            ServiceResponse<MenuDetailDTO> _response = new();
            try
            {
                var _menuDetailList = await _menuDetailRepo.GetMenuDetailByGUIDAsync(id);

                if (_menuDetailList == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                var _menuDetailDto = _mapper.Map<MenuDetailDTO>(_menuDetailList);

                _response.Status = true;
                _response.Title = "Created";
                _response.Data = _menuDetailDto;

            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }

        public async Task<ServiceResponse<string>> HardDeleteMenuDetailAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingMenuDetail = await _menuDetailRepo.GetMenuDetailByGUIDAsync(id);

                if (_existingMenuDetail == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                if (!await _menuDetailRepo.HardDeleteMenuDetailAsync(id))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to delete category!");
                    _response.Data = null;
                    return _response;
                }


                _response.Status = true;
                _response.Title = "Created";
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }

        public async Task<ServiceResponse<MenuDetailDTO>> UpdateMenuDetailAsync(MenuDetailDTO menuDetailDTO)
        {
            ServiceResponse<MenuDetailDTO> _response = new();
            try
            {
                var _existingMenuDetail= await _menuDetailRepo.GetMenuDetailByGUIDAsync(menuDetailDTO.Id);

                if (_existingMenuDetail == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }
                _existingMenuDetail.Price = menuDetailDTO.Price;
                _existingMenuDetail.MenuId = menuDetailDTO.MenuId;

                if (!await _menuDetailRepo.UpdateMenuDetailAsync(_existingMenuDetail))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to delete category!");
                    _response.Data = null;
                    return _response;
                }


                _response.Status = true;
                _response.Title = "Created";
                _response.Data = _mapper.Map<MenuDetailDTO>(_existingMenuDetail);
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Title = "Error";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Data = null;
            }
            return _response;
        }
    }
}
