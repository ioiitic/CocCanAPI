using Repository.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace Repository.repositories.imp
{
    public class BatchRepository:IBatchRepository
    {
        private readonly CocCanDBContext _dataContext;

        public BatchRepository(CocCanDBContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<bool> CreateBatchAsync(Batch batch)
        {
            await _dataContext.Batches.AddAsync(batch);
            return await Save();
        }

        public async Task<ICollection<Batch>>
            GetAllBatchesAsync()
        {
            IQueryable<Batch> _batches =
                _dataContext.Batches.Where(s => s.Status == 1);

            return await _batches
                .ToListAsync();
        }


        public async Task<Batch> GetBatchByGUIDAsync(Guid id)
        {
            return await _dataContext.Batches
                .Where(s => s.Status == 1)
                .SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> SoftDeleteBatchAsync(Guid id)
        {
            var _existingBatch = await GetBatchByGUIDAsync(id);

            if (_existingBatch != null)
            {
                _existingBatch.Status = 0;
                return await Save();
            }
            return false;
        }

        public async Task<bool> UpdateBatchAsync(Batch batch)
        {
            _dataContext.Batches.Update(batch);
            return await Save();
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
