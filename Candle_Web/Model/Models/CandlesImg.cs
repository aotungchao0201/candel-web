using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class CandlesImg
    {
        public int CandleImgId { get; set; }
        public int? CandleId { get; set; }
        public string? ImgUrl { get; set; }

        public virtual Candle? Candle { get; set; }
    }
}
