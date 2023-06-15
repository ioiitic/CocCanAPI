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
        Task<ICollection<Menu>>
            GetAllMenusWithStatusAsync(string search, int from, int to, string filter, string orderBy, bool ascending);
        Task<bool> CreateMenuAsync(Menu menu);
        Task<bool> UpdateMenuAsync(Menu menu);
        Task<bool> SoftDeleteMenuAsync(Guid id);
        Task<Menu> GetMenuByGUIDAsync(Guid id);
    }
}
