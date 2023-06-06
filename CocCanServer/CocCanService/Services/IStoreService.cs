using CocCanService.DTOs.Staff;
using CocCanService.DTOs.Store;
using CocCanService.Services.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services
{
    public interface IStoreService
    {
        Task<ServiceResponse<List<DTOs.Store.StoreDTO>>> GetAllStoresWithStatusAsync();
        Task<ServiceResponse<DTOs.Store.StoreDTO>> CreateStoreAsync(CreateStoreDTO createStoreDTO);
        Task<ServiceResponse<DTOs.Store.StoreDTO>> UpdateStoreAsync(StoreDTO storeDTO);
        Task<ServiceResponse<DTOs.Store.StoreDTO>> GetStoreByIdAsync(Guid id);
        Task<ServiceResponse<string>> SoftDeleteStoreAsync(Guid id);
    }
}
