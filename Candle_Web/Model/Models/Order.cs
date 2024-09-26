using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            VnpayTransactions = new HashSet<VnpayTransaction>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<VnpayTransaction> VnpayTransactions { get; set; }
    }
}
