using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Modals
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int CandleId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; } 
    }
}
