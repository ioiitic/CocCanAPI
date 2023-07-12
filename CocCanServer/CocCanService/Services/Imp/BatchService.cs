using AutoMapper;
using CocCanService.DTOs.Batch;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class BatchService:IBatchService
    {
        private readonly IBatchRepository _BatchRepo;
        private readonly IMapper _mapper;

        public BatchService(IBatchRepository BatchRepo, IMapper mapper)
        {
            this._BatchRepo = BatchRepo;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<BatchDTO>> CreateBatchAsync(CreateBatchDTO createBatchDTO)
        {
            ServiceResponse<BatchDTO> _response = new();
            try
            {
                var _newBatch = _mapper.Map<Batch>(createBatchDTO);


                if (!await _BatchRepo.CreateBatchAsync(_newBatch))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Category Repository when trying to create store!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Created";
                _response.Data = _mapper.Map<BatchDTO>(_newBatch);
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

        public async Task<ServiceResponse<List<BatchDTO>>> GetAllBatchesAsync()
        {
            ServiceResponse<List<BatchDTO>> _response = new();
            try
            {
                var _BatchList = await _BatchRepo.GetAllBatchesAsync();

                var _BatchListDTO = new List<BatchDTO>();

                foreach (var item in _BatchList)
                {
                    _BatchListDTO.Add(_mapper.Map<BatchDTO>(item));
                }

                _response.Status = true;
                _response.Title = "Got all TimeSlots";
                _response.Data = _BatchListDTO;
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

        public async Task<ServiceResponse<BatchDTO>> GetBatchByIdAsync(Guid id)
        {
            ServiceResponse<BatchDTO> _response = new();
            try
            {
                var _BatchList = await _BatchRepo.GetBatchByGUIDAsync(id);

                if (_BatchList == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                var _BatchDto = _mapper.Map<BatchDTO>(_BatchList);

                _response.Status = true;
                _response.Title = "Got TimeSlot";
                _response.Data = _BatchDto;

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

        public async Task<ServiceResponse<string>> SoftDeleteBatchAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingBatch = await _BatchRepo.GetBatchByGUIDAsync(id);

                if (_existingBatch == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                if (!await _BatchRepo.SoftDeleteBatchAsync(id))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to delete category!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Deleted TimeSlot";
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

        public async Task<ServiceResponse<BatchDTO>> UpdateBatchAsync(Guid id, UpdateBatchDTO updateBatchDTO)
        {
            ServiceResponse<BatchDTO> _response = new();
            try
            {
                var _existingBatch = await _BatchRepo.GetBatchByGUIDAsync(id);

                if (_existingBatch == null)
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Not Found!");
                    _response.Data = null;
                    return _response;
                }

                _existingBatch = _mapper.Map<Batch>(updateBatchDTO);

                if (!await _BatchRepo.UpdateBatchAsync(_existingBatch))
                {
                    _response.Status = false;
                    _response.Title = "Error";
                    _response.ErrorMessages.Add("Some error occur in Store Repository when trying to update category!");
                    _response.Data = null;
                    return _response;
                }

                _response.Status = true;
                _response.Title = "Updated category";
                _response.Data = _mapper.Map<BatchDTO>(_existingBatch);
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
