﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Entities
{
    public partial class Batch
    {
        public Guid Id { get; set; }
        public Guid StaffId { get; set; }
        public Guid SessionId { get; set; }
        public Guid StoreId { get; set; }
        public int Status { get; set; }

        public virtual Session Session { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Store Store { get; set; }
    }
}
