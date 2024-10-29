using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Modals.Respond
{
    public class LoginRespond
    {
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
