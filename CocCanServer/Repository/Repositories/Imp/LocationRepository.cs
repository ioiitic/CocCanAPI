using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.repositories.imp
{
    public class LocationRepository : ILocationRepository
    {
        private readonly CocCanDBContext _dataContext;

        public LocationRepository(CocCanDBContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<bool> CreateLocationAsync(Location location)
        {
            await _dataContext.Locations.AddAsync(location);
            return await Save();
        }

        public async Task<ICollection<Location>> GetAllLocationsAsync()
        {
            return await _dataContext.Locations.Where(e => e.Status==1).ToListAsync();
        }

        public async Task<bool> HardDeleteLocationAsync(Location location)
        {
            _dataContext.Remove(location);
            return await Save();
        }

        public async Task<bool> SoftDeleteLocationAsync(Guid id)
        {
            var _existingLocation = await GetLocationByGUIDAsync(id);

            if (_existingLocation != null)
            {
                _existingLocation.Status = 0;
                return await Save();
            }
            return false;
        }

        public Task<bool> UpdateLocationAsync(Location location)
        {
            _dataContext.Locations.Update(location);
            return Save();
        }

        public async Task<Location> GetLocationByGUIDAsync(Guid id)
        {
            return await _dataContext.Locations.SingleOrDefaultAsync(s => s.Id == id);
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
