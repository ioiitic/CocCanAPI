using CocCanService.DTOs.Session;
using CocCanService.DTOs.Staff;
using CocCanService.Services.Imp;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services
{
    public interface IStaffService
    {
        Task<ServiceResponse<List<StaffDTO>>> GetAllStaffsAsync();
        Task<ServiceResponse<StaffDTO>> CheckStaffLoginsAsync(string Email, string Password);
        Task<ServiceResponse<StaffDTO>> CreateStaffAsync(CreateStaffDTO createStaffDTO);
        Task<ServiceResponse<StaffDTO>> UpdateStaffAsync(StaffDTO staffDTO);  
        Task<ServiceResponse<string>> SoftDeleteStaffAsync(Guid id);
        Task<ServiceResponse<StaffDTO>> GetStaffByGUIDAsync(Guid id);
    }
}
