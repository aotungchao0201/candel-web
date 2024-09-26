using Model.Models;
using Service.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface ICandleService
    {
        Task<List<CandleDTO>> GetAllcandleAscyn();

        Task<CandleDTO> createCandle(CandleDTO candleDTO);  
        Task<bool> updateCandle(int id,CandleDTO dto);
        Task<bool> deleteCandle(int id);
        //Task<string> getAccountName(string email);
        //Task<User> getAccountInfoByEmail(string email);
        //Task<short> getAccountIdByAccountName(string name);
        //Task<User> getAccountInfoById(int id);

    }
}
