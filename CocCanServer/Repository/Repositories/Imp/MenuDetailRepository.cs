using Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository.repositories.imp
{
    public class MenuDetailRepository : IMenuDetailRepository
    {
        private readonly CocCanDBContext _dataContext;

        public MenuDetailRepository(CocCanDBContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<bool> CreateMenuDetailAsync(MenuDetail menuDetail)
        {
            await _dataContext.MenuDetails.AddAsync(menuDetail);
            return await Save();
        }

        public async Task<ICollection<MenuDetail>> GetAllMenuDetailsAsync()
        {
            return await _dataContext.MenuDetails.ToListAsync();
        }

        public async Task<bool> HardDeleteMenuDetailAsync(Guid id)
        {
            var _existingMenuDetail = await GetMenuDetailByGUIDAsync(id);
            _dataContext.Remove(_existingMenuDetail);
            return await Save();
        }

        //public async Task<bool> SoftDeleteMenuDetailAsync(Guid id)
        //{
        //    var _existingMenu = await GetMenuByGUIDAsync(id);

        //    if (_existingMenu != null)
        //    {
        //        _existingMenu.Status = 0;
        //        return await Save();
        //    }
        //    return false;
        //}

        public Task<bool> UpdateMenuDetailAsync(MenuDetail menuDetail)
        {
            _dataContext.MenuDetails.Update(menuDetail);
            return Save();
        }

        public async Task<MenuDetail> GetMenuDetailByGUIDAsync(Guid id)
        {
            return await _dataContext.MenuDetails.SingleOrDefaultAsync(s => s.Id == id);
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
