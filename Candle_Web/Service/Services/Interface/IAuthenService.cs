using Service.Modals.Request;
using Service.Modals.Respond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IAuthenService
    {
        Task<LoginRequest> Login(string email, string password);

    }
}
