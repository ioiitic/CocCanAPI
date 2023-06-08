using Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository.repositories.imp
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CocCanDBContext _dataContext;

        public OrderRepository(CocCanDBContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public async Task<bool> CreateOrderAsync(Order order)
        {
            await _dataContext.Orders.AddAsync(order);
            return await Save();
        }

        public async Task<ICollection<Order>> GetAllOrdersAsync()
        {
            return await _dataContext.Orders.Where(e => e.Status==1).ToListAsync();
        }

        public async Task<bool> HardDeleteOrderAsync(Order order)
        {
            _dataContext.Remove(order);
            return await Save();
        }

        public async Task<bool> SoftDeleteOrderAsync(Guid id)
        {
            var _existingOrder = await GetOrderByGUIDAsync(id);

            if (_existingOrder != null)
            {
                _existingOrder.Status = 0;
                return await Save();
            }
            return false;
        }

        public Task<bool> UpdateOrderAsync(Order order)
        {
            _dataContext.Orders.Update(order);
            return Save();
        }

        public async Task<Order> GetOrderByGUIDAsync(Guid id)
        {
            return await _dataContext.Orders.SingleOrDefaultAsync(s => s.Id == id);
        }

        private async Task<bool> Save()
        {
            return await _dataContext.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
