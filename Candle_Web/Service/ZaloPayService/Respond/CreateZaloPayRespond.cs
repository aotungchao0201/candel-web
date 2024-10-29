using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ZaloPayService.Respond
{
    public class CreateZaloPayRespond
    {
        public int returnCode { get; set; }
        public string returnMessage { get; set; } = string.Empty;
        public string orderUrl { get; set; } = string.Empty;
    }
}
