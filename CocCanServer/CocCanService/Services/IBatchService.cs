using CocCanService.DTOs.Batch;
using CocCanService.Services.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services
{
    public interface IBatchService
    {
        Task<ServiceResponse<List<DTOs.Batch.BatchDTO>>> GetAllBatchesAsync();
        Task<ServiceResponse<DTOs.Batch.BatchDTO>> CreateBatchAsync(CreateBatchDTO createBatchDTO);
        Task<ServiceResponse<DTOs.Batch.BatchDTO>> UpdateBatchAsync(Guid id, UpdateBatchDTO updateBatchDTO);
        Task<ServiceResponse<DTOs.Batch.BatchDTO>> GetBatchByIdAsync(Guid id);
        Task<ServiceResponse<string>> SoftDeleteBatchAsync(Guid id);
    }
}
