using AutoMapper;
using CocCanService.DTOs.Staff;
using CocCanService.DTOs.Store;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class StoreService:IStoreService
    {
        private readonly IStoreRepository _storeRepo;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository storeRepo, IMapper mapper)
        {
            this._storeRepo = storeRepo;
            this._mapper = mapper;
        }      

        public async Task<ServiceResponse<StoreDTO>> CreateStoreAsync(CreateStoreDTO createStoreDTO)
        {
            ServiceResponse<StoreDTO> _response = new();
            try
            {
                //var _newStore = _mapper.Map<Store>(createStoreDTO);
            
                Repository.Entities.Store _newStore = new()
                {
                    Id = Guid.NewGuid(),
                    Name = createStoreDTO.Name,
                    Status = createStoreDTO.Status
                };

                if (!await _storeRepo.CreateStoreAsync(_newStore))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Created";
                _response.Data = _mapper.Map<StoreDTO>(_newStore);
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

        public async Task<ServiceResponse<List<StoreDTO>>> GetAllStoresWithStatusAsync()
        {
            ServiceResponse<List<StoreDTO>> _response = new();
            try
            {
                var _StoreList = await _storeRepo.GetAllStoresWithStatusAsync();

                var _StoreListDTO = new List<StoreDTO>();

                foreach (var item in _StoreList)
                {
                    _StoreListDTO.Add(_mapper.Map<StoreDTO>(item));
                }

                _response.Success = true;
                _response.Data = _StoreListDTO;
                _response.Message = "OK";
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Data = null;
                _response.Message = "Error";
                _response.Error = "StoreService Error";
                _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
            }
            return _response;
        }

        public async Task<ServiceResponse<StoreDTO>> GetStoreByIdAsync(Guid id)
        {
            ServiceResponse<StoreDTO> _response = new();
            try
            {
                var _StoreList = await _storeRepo.GetStoreByGUIDAsync(id);

                if (_StoreList == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                var _StoreDto = _mapper.Map<StoreDTO>(_StoreList);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _StoreDto;

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

        public async Task<ServiceResponse<string>> SoftDeleteStoreAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingStore = await _storeRepo.GetStoreByGUIDAsync(id);
                
                if (_existingStore == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                if (!await _storeRepo.SoftDeleteStoreAsync(id))
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

        public async Task<ServiceResponse<StoreDTO>> UpdateStoreAsync(StoreDTO storeDTO)
        {
            ServiceResponse<StoreDTO> _response = new();
            try
            {
                var _existingStore = await _storeRepo.GetStoreByGUIDAsync(storeDTO.Id);

                if (_existingStore == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                _existingStore.Name = storeDTO.Name;
                _existingStore.Status = storeDTO.Status;

                if (!await _storeRepo.UpdateStoreAsync(_existingStore))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }


                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _mapper.Map<StoreDTO>(_existingStore);
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
