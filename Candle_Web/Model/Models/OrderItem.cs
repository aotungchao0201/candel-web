using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int CandleId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Candle Candle { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
