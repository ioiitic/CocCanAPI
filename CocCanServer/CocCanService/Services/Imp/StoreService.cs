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
            ServiceResponse<StoreDTO> _response = new ServiceResponse<StoreDTO>();
            try
            {
                var _newStore = _mapper.Map<Store>(createStoreDTO);

                if (!await _storeRepo.CreateStoreAsync(_newStore))
                {
                    _response.Status = false;
                    _response.Title = "RepoError";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to create store!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Created";
                _response.Data = _mapper.Map<StoreDTO>(_newStore);
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

        public async Task<ServiceResponse<List<StoreDTO>>> 
            GetAllStoresWithStatusAsync(string search, int from, int to, string filter, string orderBy, bool ascending)
        {
            ServiceResponse<List<StoreDTO>> _response = new ServiceResponse<List<StoreDTO>>();
            try
            {
                var _StoreList = await _storeRepo
                    .GetAllStoresWithStatusAsync(search, from, to, filter, orderBy, ascending);

                var _StoreListDTO = new List<StoreDTO>();

                foreach (var item in _StoreList)
                {
                    _StoreListDTO.Add(_mapper.Map<StoreDTO>(item));
                }

                _response.Status = true;
                _response.Title = "Got all stores";
                _response.Data = _StoreListDTO;
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

        public async Task<ServiceResponse<StoreDTO>> GetStoreByIdAsync(Guid id)
        {
            ServiceResponse<StoreDTO> _response = new ServiceResponse<StoreDTO>();
            try
            {
                var _StoreList = await _storeRepo.GetStoreByGUIDAsync(id);

                if (_StoreList == null)
                {
                    _response.Status = false;
                    _response.Title = "NotFound";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                var _StoreDto = _mapper.Map<StoreDTO>(_StoreList);

                _response.Status = true;
                _response.Title = "Got store";
                _response.Data = _StoreDto;

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

        public async Task<ServiceResponse<string>> SoftDeleteStoreAsync(Guid id)
        {
            ServiceResponse<string> _response = new ServiceResponse<string>();
            try
            {
                var _existingStore = await _storeRepo.GetStoreByGUIDAsync(id);
                
                if (_existingStore == null)
                {
                    _response.Status = false;
                    _response.Title = "NotFound";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                if (!await _storeRepo.SoftDeleteStoreAsync(id))
                {
                    _response.Status = false;
                    _response.Title = "RepoError";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to delete store!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Deleted store";
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

        public async Task<ServiceResponse<StoreDTO>> UpdateStoreAsync(Guid id, UpdateStoreDTO updateStoreDTO)
        {
            ServiceResponse<StoreDTO> _response = new ServiceResponse<StoreDTO>();
            try
            {
                var _existingStore = await _storeRepo.GetStoreByGUIDAsync(id);

                if (_existingStore == null)
                {
                    _response.Status = false;
                    _response.Title = "NotFound";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                _existingStore = _mapper.Map<Store>(updateStoreDTO);
                //if (UpdateStoreDTO.Name != "")
                //    _existingStore.Name = UpdateStoreDTO.Name;
                //if (UpdateStoreDTO.Image != "")
                //    _existingStore.Image = UpdateStoreDTO.Image;

                if (!await _storeRepo.UpdateStoreAsync(_existingStore))
                {
                    _response.Status = false;
                    _response.Title = "RepoError";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to update store!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Updated store";
                _response.Data = _mapper.Map<StoreDTO>(_existingStore);
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
