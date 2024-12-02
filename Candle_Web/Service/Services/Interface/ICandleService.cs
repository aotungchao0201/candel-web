using Model.Models;
using Service.Modals;
using Service.Modals.Request;
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

        Task<CandleRequest> createCandle(CandleRequest candleDTO);  
        Task<bool> updateCandle(int id, CandleRequest dto);
        Task<bool> deleteCandle(int id);
        Task<List<CandleDTO>> GetAllCandleByCategory(int id);
        //Task<string> getAccountName(string email);
        //Task<User> getAccountInfoByEmail(string email);
        Task<CandleDTO> GetByid(int name);
        //Task<User> getAccountInfoById(int id);

    }
}
