﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Modals.Request
{
    public class OrderRequestDTO
    {
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Address { get; set; }
        public int? Phone { get; set; }
        public string? Note { get; set; }
        public string? IsPay { get; set; }
    }
}
