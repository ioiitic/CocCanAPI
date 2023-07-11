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

        public async Task<ICollection<OrderDetail>> GetAllOrderDetailsAsync(Dictionary<string, List<string>> filter, int from, int to, string orderBy, bool ascending)
        {
            IQueryable<OrderDetail> _orderdetails =
                _dataContext.OrderDetails.Where(s => s.Status == 1);

            //var stores = _stores
            //    .Join(_dataContext.Products, s => s.Id, p => p.StoreId, (s,p) => new { s = s, p = p });



            if (from <= to & from > 0)
                _orderdetails = _orderdetails.Skip(from - 1).Take(to - from + 1);

            if (filter != null)
                foreach (KeyValuePair<string, List<string>> filterIte in filter)
                {
                    switch (filterIte.Key)
                    {
                        case "orderid":
                            _orderdetails = _orderdetails
                                .Where(m => filterIte.Value.Any(fi => m.OrderId.ToString() == fi))
                                .Distinct();
                            break;
                    }
                }
            return await _orderdetails
                .ToListAsync();
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
