using CocCanService.DTOs.Product;
using CocCanService.Services.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.Services
{
    public interface IProductService
    {
        Task<ServiceResponse<List<SessionDTO>>> GetAllProductsAsync();
        Task<ServiceResponse<SessionDTO>> CreateProductAsync(CreateSessionDTO createProductDTO);
        Task<ServiceResponse<SessionDTO>> UpdateProductAsync(SessionDTO productDTO);
        Task<ServiceResponse<string>> SoftDeleteProductAsync(Guid id);
        Task<ServiceResponse<bool>> HardDeleteProductAsync(SessionDTO productDTO);
        Task<ServiceResponse<SessionDTO>> GetProductByGUIDAsync(Guid id);
    }
}
