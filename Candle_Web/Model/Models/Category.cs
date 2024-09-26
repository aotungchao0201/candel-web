using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Category
    {
        public Category()
        {
            Candles = new HashSet<Candle>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Candle> Candles { get; set; }
    }
}
