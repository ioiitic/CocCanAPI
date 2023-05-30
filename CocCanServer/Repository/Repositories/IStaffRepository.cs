using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repositories
{
    public interface IStaffRepository
    {
        Task<ICollection<Staff>> GetAllStaffsAsync();
        Task<Staff> CheckStaffLoginsAsync(string Email, string Password);
        Task<bool> CreateStaffAsync(Staff staff);
        Task<bool> UpdateStaffAsync(Staff staff);
        Task<bool> SoftDeleteStaffAsync(Guid id);
        Task<bool> HardDeleteStaffAsync(Staff staff);
        Task<Staff> GetStaffByGUIDAsync(Guid id);

    }
}
