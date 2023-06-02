using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repositories
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAllProductsAsync();
        Task<bool> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> SoftDeleteProductAsync(Guid id);
        Task<bool> HardDeleteProductAsync(Product product);
        Task<Product> GetProductByGUIDAsync(Guid id);
    }
}
