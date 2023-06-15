﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
