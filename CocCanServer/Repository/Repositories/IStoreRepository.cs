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
        Task<ICollection<Store>> 
            GetAllStoresWithStatusAsync(string search, int from, int to, string filter, string orderBy, bool ascending);
        Task<bool> CreateStoreAsync(Store store);
        Task<bool> UpdateStoreAsync(Store store);
        Task<bool> SoftDeleteStoreAsync(Guid id);
        Task<Store> GetStoreByGUIDAsync(Guid id);
    }
}
