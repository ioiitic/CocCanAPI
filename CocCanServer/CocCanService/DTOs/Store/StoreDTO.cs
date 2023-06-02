using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Store
{
    public class StoreDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
