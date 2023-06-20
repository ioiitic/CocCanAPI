using AutoMapper;
using CocCanService.DTOs.MenuDetail;
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
                Repository.Entities.MenuDetail _newMenuDetail = new()
                {
                    Id = Guid.NewGuid(),
                    Price = createMenuDetailDTO.Price,
                    MenuId = createMenuDetailDTO.MenuId,
                    ProductId = createMenuDetailDTO.ProductId,
                };

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

        public async Task<ServiceResponse<List<MenuDetailDTO>>> GetAllMenuDetailsAsync()
        {
            ServiceResponse<List<MenuDetailDTO>> _response = new();
            try
            {
                var _MenuDetailList = await _menuDetailRepo.GetAllMenuDetailsAsync();

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
                _existingMenuDetail.ProductId = menuDetailDTO.ProductId;
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
