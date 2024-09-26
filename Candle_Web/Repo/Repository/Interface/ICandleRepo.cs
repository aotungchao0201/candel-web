using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repository.Interface
{
    public interface ICandleRepo
    {
        public Task<List<Candle>> GetAllCandle();
        public Task<Candle> CreateCandle(Candle candle);
        public Task<Candle> UpdateCandle(Candle candle);
        public Task<bool> DeleteCandle(Candle candle);

        public Task<Candle> GetCandleById(int id);
        public Task<Candle> GetCandleByName(string? candle);
    }
}
