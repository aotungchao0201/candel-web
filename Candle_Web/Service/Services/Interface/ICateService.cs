using Model.Models;
using Service.Modals.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface ICateService
    {
        public Task<List<Category>> GetALL();
        public Task<CateRequest> Create(CateRequest order);
        public Task<bool> Delete(int order);

        public Task<List<Candle>> GetCandleByCategoryId(int id);
    }
}
