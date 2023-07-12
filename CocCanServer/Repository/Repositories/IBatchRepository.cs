using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repositories
{
    public interface IBatchRepository
    {
        Task<ICollection<Batch>> GetAllBatchesAsync();
        Task<bool> CreateBatchAsync(Batch batch);
        Task<bool> UpdateBatchAsync(Batch batch);
        Task<bool> SoftDeleteBatchAsync(Guid id);
        Task<Batch> GetBatchByGUIDAsync(Guid id);
    }
}
