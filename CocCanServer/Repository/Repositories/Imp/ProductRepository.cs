using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.repositories.imp
{
    public class ProductRepository : IProductRepository
    {
        private readonly CocCanDBContext _dataContext;

        public ProductRepository(CocCanDBContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            await _dataContext.Products.AddAsync(product);
            return await Save();
        }

        public async Task<ICollection<Product>> 
            GetAllProductsAsync()
        {
            IQueryable<Product> _products = 
                _dataContext.Products
                .Where(p => p.Status == 1);

            return await _products
                .Include(p => p.Store)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetProductByGUIDAsync(Guid id)
        {
            return await _dataContext.Products
                .Where(p => p.Status == 1)
                .Include(p => p.Store)
                .Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> HardDeleteProductAsync(Product product)
        {
            _dataContext.Products.Remove(product);
            return await Save();
        }

        public async Task<bool> SoftDeleteProductAsync(Guid id)
        {
            var _existingProduct = await GetProductByGUIDAsync(id);

            if (_existingProduct != null)
            {
                _existingProduct.Status = 0;
                return await Save();
            }
            return false;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _dataContext.Products.Update(product);
            return await Save();
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
