using Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository.repositories.imp
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly CocCanDBContext _dataContext;

        public CategoryRepository(CocCanDBContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<bool> CreateCategoryAsync(Category category)
        {
            await _dataContext.Categories.AddAsync(category);
            return await Save();
        }

        public async Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            return await _dataContext.Categories.Where(e => e.Status==1).ToListAsync();
        }

        public async Task<bool> HardDeleteCategoryAsync(Category category)
        {
            _dataContext.Remove(category);
            return await Save();
        }

        public async Task<bool> SoftDeleteCategoryAsync(Guid id)
        {
            var _existingCategory = await GetCategoryByGUIDAsync(id);

            if (_existingCategory != null)
            {
                _existingCategory.Status = 0;
                return await Save();
            }
            return false;
        }

        public Task<bool> UpdateCategoryAsync(Category category)
        {
            _dataContext.Categories.Update(category);
            return Save();
        }

        public async Task<Category> GetCategoryByGUIDAsync(Guid id)
        {
            return await _dataContext.Categories.SingleOrDefaultAsync(s => s.Id == id);
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
