using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repositories
{
    public interface IMenuRepository
    {
        Task<ICollection<Menu>> GetAllMenusAsync();
        Task<bool> CreateMenuAsync(Menu menu);
        Task<bool> UpdateMenuAsync(Menu menu);
        Task<bool> SoftDeleteMenuAsync(Guid id);
        Task<bool> HardDeleteMenuAsync(Menu menu);
        Task<Menu> GetMenuByGUIDAsync(Guid id);
    }
}
