using AutoMapper;
using CocCanService.DTOs.Menu;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class MenuService: IMenuService
    {
        private readonly IMenuRepository _menuRepo;
        private readonly IMapper _mapper;

        public MenuService(IMenuRepository menuRepo, IMapper mapper)
        {
            this._menuRepo = menuRepo;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<MenuDTO>> CreateMenuAsync(CreateMenuDTO createMenuDTO)
        {
            ServiceResponse<MenuDTO> _response = new();
            try
            {
                Repository.Entities.Menu _newMenu = new()
                {
                    Id = Guid.NewGuid(),
                    Status = createMenuDTO.Status
                };

                if (!await _menuRepo.CreateMenuAsync(_newMenu))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Created";
                _response.Data = _mapper.Map<MenuDTO>(_newMenu);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<List<MenuDTO>>> GetAllMenusAsync()
        {
            ServiceResponse<List<MenuDTO>> _response = new();
            try
            {
                var _MenuList = await _menuRepo.GetAllMenusAsync();

                var _MenuListDTO = new List<MenuDTO>();

                foreach (var item in _MenuList)
                {
                    _MenuListDTO.Add(_mapper.Map<MenuDTO>(item));
                }

                _response.Success = true;
                _response.Data = _MenuListDTO;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<MenuDTO>> GetMenuByIdAsync(Guid id)
        {
            ServiceResponse<MenuDTO> _response = new();
            try
            {
                var _menuList = await _menuRepo.GetMenuByGUIDAsync(id);

                if (_menuList == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                var _menuDto = _mapper.Map<MenuDTO>(_menuList);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _menuDto;

            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<string>> SoftDeleteMenuAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingMenu = await _menuRepo.GetMenuByGUIDAsync(id);

                if (_existingMenu == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                if (!await _menuRepo.SoftDeleteMenuAsync(id))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    return _response;
                }


                _response.Success = true;
                _response.Message = "SoftDeleted";
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<MenuDTO>> UpdateMenuAsync(MenuDTO menuDTO)
        {
            ServiceResponse<MenuDTO> _response = new();
            try
            {
                var _existingMenu = await _menuRepo.GetMenuByGUIDAsync(menuDTO.Id);

                if (_existingMenu == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }
                _existingMenu.Status = menuDTO.Status;

                if (!await _menuRepo.UpdateMenuAsync(_existingMenu))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }


                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _mapper.Map<MenuDTO>(_existingMenu);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }
    }
}
