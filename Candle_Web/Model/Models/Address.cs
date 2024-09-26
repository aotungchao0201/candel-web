using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string AddressLine { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? PostalCode { get; set; }
        public string Country { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
