using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Entities
{
    public partial class OrderDetail
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
<<<<<<< HEAD
        public Guid MenuDetailId { get; set; }
=======
        public Guid ProductId { get; set; }
>>>>>>> NT2
        public Guid OrderId { get; set; }
        public int Status { get; set; }

        public virtual MenuDetail MenuDetail { get; set; }
        public virtual Order Order { get; set; }
    }
}
