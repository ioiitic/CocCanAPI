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
        Task<ServiceResponse<List<DTOs.Staff.StaffDTO>>> GetAllStaffsAsync();
        Task<ServiceResponse<DTOs.Staff.StaffDTO>> CheckStaffLoginsAsync(string Email, string Password);
        Task<ServiceResponse<DTOs.Staff.StaffDTO>> CreateStaffAsync(CreateStaffDTO createStaffDTO);
        Task<ServiceResponse<DTOs.Staff.StaffDTO>> UpdateStaffAsync(StaffDTO staffDTO);  
        Task<ServiceResponse<string>> SoftDeleteStaffAsync(Guid id);
    }
}
