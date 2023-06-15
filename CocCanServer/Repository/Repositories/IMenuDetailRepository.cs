using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repositories
{
    public interface IMenuDetailRepository
    {
        Task<ICollection<MenuDetail>>
            GetAllMenuDetailsWithStatusAsync(string search, int from, int to, string filter, string orderBy, bool ascending);
        Task<bool> CreateMenuDetailAsync(MenuDetail menuDetail);
        Task<bool> UpdateMenuDetailAsync(MenuDetail menuDetail);
        Task<bool> SoftDeleteMenuDetailAsync(Guid id);
        Task<MenuDetail> GetMenuDetailByGUIDAsync(Guid id);
    }
}
