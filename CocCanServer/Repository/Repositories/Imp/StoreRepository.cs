using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.repositories.imp
{
    public class StoreRepository : IStoreRepository
    {
        private readonly CocCanDBContext _dataContext;

        public StoreRepository(CocCanDBContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<bool> CreateStoreAsync(Store store)
        {
            await _dataContext.Stores.AddAsync(store);
            return await Save();
        }

        public async Task<ICollection<Store>> 
            GetAllStoresWithStatusAsync
            (string search, int from, int to, string filter, string orderBy, bool ascending)
        {
            IQueryable<Store>  _stores = 
                _dataContext.Stores.Where(s => s.Status == 1);

            var stores = _stores
                .Join(_dataContext.Products, s => s.Id, p => p.StoreId, (s,p) => new { s = s, p = p });

            if (search != "" && search != null)
                _stores = _stores.Where(s => s.Name.Contains(search));

            switch (orderBy)
            {
                case "Name":
                    if (ascending)
                        _stores = _stores.OrderBy(s => s.Name);
                    else
                        _stores = _stores.OrderByDescending(s => s.Name);
                    break;
                default:
                    if (ascending)
                        _stores = _stores.OrderBy(s => s.Id);
                    else
                        _stores = _stores.OrderByDescending(s => s.Id);
                    break;
            }

            if (from <= to & from > 0)
                _stores = _stores.Skip(from - 1).Take(to - from + 1);

            return await _stores
                .ToListAsync();
        }

        public async Task<Store> GetStoreByGUIDAsync(Guid id)
        {
            return await _dataContext.Stores
                .Where(s => s.Status == 1)
                .Include(s => s.Products)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> SoftDeleteStoreAsync(Guid id)
        {
            var _existingStore = await GetStoreByGUIDAsync(id);

            if (_existingStore != null)
            {
                _existingStore.Status = 0;
                return await Save();
            }
            return false;
        }

        public async Task<bool> UpdateStoreAsync(Store store)
        {
            _dataContext.Stores.Update(store);
            return await Save();
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
