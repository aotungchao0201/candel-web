using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class VnpayTransaction
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public string VnpTransactionId { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
