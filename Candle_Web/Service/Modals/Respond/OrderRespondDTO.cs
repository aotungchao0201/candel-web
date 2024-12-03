using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Modals.Respond
{
    public class OrderRespondDTO
    {
        public int OrderId { get; set; } // Identifier for the user placing the order

        public string Username { get; set; } // Identifier for the user placing the order
        public decimal TotalPrice { get; set; } // Total price of the order
        public string? Status { get; set; } // Status of the order (e.g., "pending", "completed")
        public DateTime? CreatedAt { get; set; } // Date when the order was created
        public string? Address { get; set; }
        public int? Phone { get; set; } // Change from int to string
        public string? Note { get; set; }
        public string? IsPay { get; set; }

        public List<OrderItemRespondDto> OrderItems { get; set; } = new List<OrderItemRespondDto>();
    }
    public class OrderItemRespondDto
    {
        public string CandleName { get; set; }
        public int Quantity { get; set; }
        public int priceItem { get; set; }


    }
}
