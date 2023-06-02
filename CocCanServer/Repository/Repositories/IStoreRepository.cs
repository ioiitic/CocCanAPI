using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repositories
{
    public interface IStoreRepository
    {
        Task<ICollection<Store>> GetAllStoresAsync();
        Task<bool> CreateStoreAsync(Store store);
        Task<bool> UpdateStoreAsync(Store store);
        Task<bool> SoftDeleteStoreAsync(Guid id);
        Task<bool> HardDeleteStoreAsync(Store store);
        Task<Store> GetStoreByGUIDAsync(Guid id);
    }
}
