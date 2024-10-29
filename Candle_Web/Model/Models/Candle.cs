using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Candle
    {
        public Candle()
        {
            CandlesImgs = new HashSet<CandlesImg>();
            OrderItems = new HashSet<OrderItem>();
            Reviews = new HashSet<Review>();
        }

        public int CandleId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? StockQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CategoryId { get; set; }
        public string? ImgUrl { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<CandlesImg> CandlesImgs { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
