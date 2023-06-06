using AutoMapper;
using CocCanService.DTOs.Location;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class LocationService:ILocationService
    {
        private readonly ILocationRepository _locationRepo;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepo, IMapper mapper)
        {
            this._locationRepo = locationRepo;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<LocationDTO>> CreateLocationAsync(CreateLocationDTO createLocationDTO)
        {
            ServiceResponse<LocationDTO> _response = new();
            try
            {
                Repository.Entities.Location _newLocation = new()
                {
                    Id = Guid.NewGuid(),
                    Name = createLocationDTO.Name,
                    Address = createLocationDTO.Address,
                    Status = createLocationDTO.Status
                };

                if (!await _locationRepo.CreateLocationAsync(_newLocation))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }

                _response.Success = true;
                _response.Message = "Created";
                _response.Data = _mapper.Map<LocationDTO>(_newLocation);
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

        public async Task<ServiceResponse<List<LocationDTO>>> GetAllLocationsAsync()
        {
            ServiceResponse<List<LocationDTO>> _response = new();
            try
            {
                var _LocationList = await _locationRepo.GetAllLocationsAsync();

                var _LocationListDTO = new List<LocationDTO>();

                foreach (var item in _LocationList)
                {
                    _LocationListDTO.Add(_mapper.Map<LocationDTO>(item));
                }

                _response.Success = true;
                _response.Data = _LocationListDTO;
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

        public async Task<ServiceResponse<LocationDTO>> GetLocationByIdAsync(Guid id)
        {
            ServiceResponse<LocationDTO> _response = new();
            try
            {
                var _LocationList = await _locationRepo.GetLocationByGUIDAsync(id);

                if (_LocationList == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    return _response;
                }

                var _LocationDto = _mapper.Map<LocationDTO>(_LocationList);

                _response.Success = true;
                _response.Message = "ok";
                _response.Data = _LocationDto;

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

        public async Task<ServiceResponse<string>> SoftDeleteLocationAsync(Guid id)
        {
            ServiceResponse<string> _response = new();
            try
            {
                var _existingLocation = await _locationRepo.GetLocationByGUIDAsync(id);

                if (_existingLocation == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                if (!await _locationRepo.SoftDeleteLocationAsync(id))
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

        public async Task<ServiceResponse<LocationDTO>> UpdateLocationAsync(LocationDTO locationDTO)
        {
            ServiceResponse<LocationDTO> _response = new();
            try
            {
                var _existingLocation = await _locationRepo.GetLocationByGUIDAsync(locationDTO.Id);

                if (_existingLocation == null)
                {
                    _response.Success = false;
                    _response.Message = "NotFound";
                    _response.Data = null;
                    return _response;
                }

                _existingLocation.Name = locationDTO.Name;
                _existingLocation.Address = locationDTO.Address;
                _existingLocation.Status = locationDTO.Status;

                if (!await _locationRepo.UpdateLocationAsync(_existingLocation))
                {
                    _response.Success = false;
                    _response.Message = "RepoError";
                    _response.Data = null;
                    return _response;
                }


                _response.Success = true;
                _response.Message = "Updated";
                _response.Data = _mapper.Map<LocationDTO>(_existingLocation);
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
