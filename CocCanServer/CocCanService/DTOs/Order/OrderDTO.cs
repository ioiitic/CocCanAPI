using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Order
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid CustomerId { get; set; }
        public Guid SessionId { get; set; }
        public int Status { get; set; }
    }
}
