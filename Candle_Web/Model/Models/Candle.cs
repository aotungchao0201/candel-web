using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Candle
    {
        public Candle()
        {
            OrderItems = new HashSet<OrderItem>();
            Reviews = new HashSet<Review>();
            Categories = new HashSet<Category>();
        }

        public int CandleId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? StockQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
