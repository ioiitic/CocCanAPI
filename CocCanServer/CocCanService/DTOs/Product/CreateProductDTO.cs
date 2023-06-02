using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Product
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public Guid StoreId { get; set; }
    }
}
