using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.OrderDetail
{
    public class CreateOrderDetailDTO
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public int Status { get; set; }
    }
}
