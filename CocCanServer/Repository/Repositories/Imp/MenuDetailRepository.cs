using Repository.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace Repository.repositories.imp
{
    public class MenuDetailRepository
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

        public async Task<ICollection<MenuDetail>>
            GetAllMenuDetailsWithStatusAsync(string search, int from, int to, string filter, string orderBy, bool ascending)
        {
            IQueryable<MenuDetail> _menuDetails =
                _dataContext.MenuDetails.Include(md => md.Product);

            return await _menuDetails
                .ToListAsync();
        }

        public async Task<MenuDetail> GetMenuDetailByGUIDAsync(Guid id)
        {
            return await _dataContext.MenuDetails
                .Include(s => s.Product)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> SoftDeleteMenuDetailAsync(Guid id)
        {
            var _existingMenuDetail = await GetMenuDetailByGUIDAsync(id);

            if (_existingMenuDetail != null)
            {
                return await Save();
            }
            return false;
        }

        public async Task<bool> UpdateMenuDetailAsync(MenuDetail MenuDetail)
        {
            _dataContext.MenuDetails.Update(MenuDetail);
            return await Save();
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
