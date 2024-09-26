using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Log
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
