using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int CandleId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Candle Candle { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
