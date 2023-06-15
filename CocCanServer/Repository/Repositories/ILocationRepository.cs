using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repositories
{
    public interface ILocationRepository
    {
        Task<ICollection<Location>>
            GetAllLocationsWithStatusAsync(string search, int from, int to, string filter, string orderBy, bool ascending);
        Task<bool> CreateLocationAsync(Location location);
        Task<bool> UpdateLocationAsync(Location location);
        Task<bool> SoftDeleteLocationAsync(Guid id);
        Task<Location> GetLocationByGUIDAsync(Guid id);
    }
}
