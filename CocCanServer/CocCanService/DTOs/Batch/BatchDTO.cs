﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocCanService.DTOs.Batch
{
    public class BatchDTO
    {
        public Guid Id { get; set; }
        public Guid StaffId { get; set; }
        public Guid SessionId { get; set; }
        public Guid StoreId { get; set; }
        public int Status { get; set; }
    }
}