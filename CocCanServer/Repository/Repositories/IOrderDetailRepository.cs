using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repositories
{
    public interface IOrderDetailRepository
    {
        Task<ICollection<OrderDetail>> GetAllOrderDetailsAsync();
        Task<bool> CreateOrderDetailAsync(OrderDetail orderDetail);
        Task<bool> UpdateOrderDetailAsync(OrderDetail orderDetail);
        Task<bool> SoftDeleteOrderDetailAsync(Guid id);
        Task<bool> HardDeleteOrderDetailAsync(OrderDetail orderDetail);
        Task<OrderDetail> GetOrderDetailByGUIDAsync(Guid id);
    }
}
