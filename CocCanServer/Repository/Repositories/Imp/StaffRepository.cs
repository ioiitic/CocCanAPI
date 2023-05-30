using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.repositories.imp
{
    public class StaffRepository : IStaffRepository
    {
        private readonly CocCanDBContext _dataContext;

        public StaffRepository(CocCanDBContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<Staff> CheckStaffLoginsAsync(string Email, string Password)
        {
            return await _dataContext.Staff.SingleOrDefaultAsync(sta => sta.Email == Email && sta.Password == Password);
        }

        public async Task<bool> CreateStaffAsync(Staff staff)
        {
            await _dataContext.Staff.AddAsync(staff);
            return await Save();
        }

        public async Task<ICollection<Staff>> GetAllStaffsAsync()
        {
            return await _dataContext.Staff.ToListAsync();
        }

        public async Task<bool> HardDeleteStaffAsync(Staff staff)
        {
            _dataContext.Remove(staff);
            return await Save();
        }

        public async Task<bool> SoftDeleteStaffAsync(Guid id)
        {
            var _existingStaff = await GetStaffByGUIDAsync(id);
            
            if (!(_existingStaff != null))
            {
                _existingStaff.Status = 0;
                return await Save();
            }
            return false;
        }

        public Task<bool> UpdateStaffAsync(Staff staff)
        {
            _dataContext.Staff.Update(staff);
            return Save();
        }

        public async Task<Staff> GetStaffByGUIDAsync(Guid id)
        {
            return await _dataContext.Staff.SingleOrDefaultAsync(s => s.Id == id);
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
