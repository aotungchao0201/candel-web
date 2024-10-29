using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Service.Modals.Request
{
    public class CandleRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? StockQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CategoryId { get; set; }
        public IFormFile ImgFile { get; set; }
        [JsonIgnore]
        public string? ImgUrl { get; set; }



    }
}
