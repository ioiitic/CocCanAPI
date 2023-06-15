using AutoMapper;
using CocCanService.DTOs.Staff;
using Repository.Entities;
using Repository.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services.Imp
{
    public class StaffService : IStaffService
    {
        //private readonly IStaffRepository _staffRepo;
        //private readonly IMapper _mapper;

        //public StaffService(IStaffRepository staffRepo, IMapper mapper)
        //{
        //    _staffRepo = staffRepo;
        //    _mapper = mapper;
        //}

        //public async Task<ServiceResponse<StaffDTO>> CheckStaffLoginsAsync(string Email, string Password)
        //{
        //    ServiceResponse<StaffDTO> _response = new();
        //    try
        //    {
        //        var _Staff = await _staffRepo.CheckStaffLoginsAsync(Email, Password);
        //        if (_Staff == null)
        //        {
        //            _response.Success = false;
        //            _response.Message = "Not Found!";
        //            return _response;
        //        } else
        //        {
        //            var _StaffDTO = _mapper.Map<StaffDTO>(_Staff);

        //            _response.Success = true;
        //            _response.Message = "OK";
        //            _response.Data= _StaffDTO;
        //        }
        //    } catch (Exception ex)
        //    {
        //        _response.Success = false;
        //        _response.Data = null;
        //        _response.Message = "Error";
        //        _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
        //    }
        //    return _response;   
        //}

        //public async Task<ServiceResponse<StaffDTO>> CreateStaffAsync(CreateStaffDTO createStaffDTO)
        //{
        //    ServiceResponse<StaffDTO> _response = new();
        //    try
        //    {
        //        var _newStaff = _mapper.Map<Staff>(createStaffDTO);
        //        if (!await _staffRepo.CreateStaffAsync(_newStaff))
        //        {
        //            _response.Success = false;
        //            _response.Message = "RepoError";
        //            _response.Data = null;
        //            return _response;
        //        }

        //        _response.Success = true;
        //        _response.Message = "Created";
        //        _response.Data = _mapper.Map<StaffDTO>(_newStaff);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.Success = false;
        //        _response.Data = null;
        //        _response.Message = "Error";
        //        _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
        //    }
        //    return _response;
        //}

        //public async Task<ServiceResponse<List<StaffDTO>>> GetAllStaffsAsync()
        //{
        //    ServiceResponse<List<StaffDTO>> _response = new();
        //    try
        //    {
        //        var _StaffList = await _staffRepo.GetAllStaffsAsync();

        //        var _StaffListDTO = new List<StaffDTO>();

        //        foreach (var item in _StaffList)
        //        {
        //            _StaffListDTO.Add(_mapper.Map<StaffDTO>(item));
        //        }

        //        _response.Success = true;
        //        _response.Data = _StaffListDTO;
        //        _response.Message = "OK";
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.Success = false;
        //        _response.Data = null;
        //        _response.Message = "Error";
        //        _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
        //    }
        //    return _response;
        //}

        //public async Task<ServiceResponse<string>> SoftDeleteStaffAsync(Guid id)
        //{
        //    ServiceResponse<string> _response = new();
        //    try
        //    {
        //        var _existingStaff = await _staffRepo.GetStaffByGUIDAsync(id);
        //        if (_existingStaff == null)
        //        {
        //            _response.Success = false;
        //            _response.Message = "NotFound";
        //            _response.Data = null;
        //            return _response;
        //        }

        //        if (!await _staffRepo.SoftDeleteStaffAsync(id))
        //        {
        //            _response.Success = false;
        //            _response.Message = "RepoError";
        //            _response.Data = null;
        //            return _response;
        //        }


        //        _response.Success = true;
        //        _response.Message = "SoftDeleted";
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.Success = false;
        //        _response.Data = null;
        //        _response.Message = "Error";
        //        _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
        //    }
        //    return _response;
        //}

        //public async Task<ServiceResponse<StaffDTO>> UpdateStaffAsync(StaffDTO staffDTO)
        //{
        //    ServiceResponse<StaffDTO> _response = new();
        //    try
        //    {
        //        var _existingStaff = await _staffRepo.GetStaffByGUIDAsync(staffDTO.Id)
        //            ;
        //        if (_existingStaff == null)
        //        {
        //            _response.Success = false;
        //            _response.Message = "NotFound";
        //            _response.Data = null;
        //            return _response;
        //        }

        //        _existingStaff.Fullname = staffDTO.Fullname;
        //        _existingStaff.Phone = staffDTO.Phone;

        //        if (!await _staffRepo.UpdateStaffAsync(_existingStaff))
        //        {
        //            _response.Success = false;
        //            _response.Message = "RepoError";
        //            _response.Data = null;
        //            return _response;
        //        }


        //        _response.Success = true;
        //        _response.Message = "Updated";
        //        _response.Data = _mapper.Map<StaffDTO>(_existingStaff);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.Success = false;
        //        _response.Data = null;
        //        _response.Message = "Error";
        //        _response.ErrorMessages = new List<string>() { Convert.ToString(ex.Message) };
        //    }
        //    return _response;
        //}
        public Task<ServiceResponse<StaffDTO>> CheckStaffLoginsAsync(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<StaffDTO>> CreateStaffAsync(CreateStaffDTO createStaffDTO)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<StaffDTO>>> GetAllStaffsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> SoftDeleteStaffAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<StaffDTO>> UpdateStaffAsync(StaffDTO staffDTO)
        {
            throw new NotImplementedException();
        }
    }
}
