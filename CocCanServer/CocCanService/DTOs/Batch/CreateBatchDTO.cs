using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Batch
{
    public class CreateBatchDTO
    {
        public Guid StaffId { get; set; }
        public Guid SessionId { get; set; }
        public Guid StoreId { get; set; }
        public int Status { get; set; }
    }
}
