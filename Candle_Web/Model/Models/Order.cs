using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Address { get; set; }
        public int? Phone { get; set; }
        public string? Note { get; set; }
        public string? IsPay { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
