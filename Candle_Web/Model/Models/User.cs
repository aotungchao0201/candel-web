using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class User
    {
        public User()
        {
            Addresses = new HashSet<Address>();
            Logs = new HashSet<Log>();
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int RoleId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
