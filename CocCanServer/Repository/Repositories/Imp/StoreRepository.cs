using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
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

        public async Task<ICollection<Store>> GetAllStoresAsync()
        {
            return await _dataContext.Stores.ToListAsync();
        }

        public async Task<bool> HardDeleteStoreAsync(Store store)
        {
            _dataContext.Remove(store);
            return await Save();
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

        public Task<bool> UpdateStoreAsync(Store store)
        {
            _dataContext.Stores.Update(store);
            return Save();
        }

        public async Task<Store> GetStoreByGUIDAsync(Guid id)
        {
            return await _dataContext.Stores.SingleOrDefaultAsync(s => s.Id == id);
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
