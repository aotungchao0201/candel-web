using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Zalopay
    {
        public int? Appid { get; set; }
        public string? Appuser { get; set; }
        public long? Apptime { get; set; }
        public long? Amount { get; set; }
        public string? Apptransid { get; set; }
        public string? Embeddata { get; set; }
        public string? Mac { get; set; }
        public string? Paymentcode { get; set; }
        public string? Bankcode { get; set; }
        public string? Description { get; set; }
        public string? Returnurl { get; set; }
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }
    }
}
