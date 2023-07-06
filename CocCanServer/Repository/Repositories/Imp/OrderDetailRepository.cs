using Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository.repositories.imp
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly CocCanDBContext _dataContext;

        public OrderDetailRepository(CocCanDBContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<bool> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            await _dataContext.OrderDetails.AddAsync(orderDetail);
            return await Save();
        }

        public async Task<ICollection<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _dataContext.OrderDetails.Where(e => e.Status==1).ToListAsync();
        }

        public async Task<bool> HardDeleteOrderDetailAsync(OrderDetail orderDetail)
        {
            _dataContext.Remove(orderDetail);
            return await Save();
        }

        public async Task<bool> SoftDeleteOrderDetailAsync(Guid id)
        {
            var _existingOrderDetail = await GetOrderDetailByGUIDAsync(id);

            if (_existingOrderDetail != null)
            {
                _existingOrderDetail.Status = 0;
                return await Save();
            }
            return false;
        }

        public Task<bool> UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            _dataContext.OrderDetails.Update(orderDetail);
            return Save();
        }

        public async Task<OrderDetail> GetOrderDetailByGUIDAsync(Guid id)
        {
            return await _dataContext.OrderDetails.SingleOrDefaultAsync(s => s.Id == id);
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }

        public async Task<ICollection<OrderDetail>> GetAllOrderDetailsByOrderIDAsync(Guid orderId)
        {
            return await _dataContext.OrderDetails.Where(e => e.OrderId == orderId).ToListAsync();
        }
    }
}
