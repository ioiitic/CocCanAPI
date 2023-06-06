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
        Task<ICollection<Location>> GetAllLocationsAsync();
        Task<bool> CreateLocationAsync(Location location);
        Task<bool> UpdateLocationAsync(Location location);
        Task<bool> SoftDeleteLocationAsync(Guid id);
        Task<bool> HardDeleteLocationAsync(Location location);
        Task<Location> GetLocationByGUIDAsync(Guid id);
    }
}
