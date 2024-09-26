using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repository
{
    public class CandleRepo : ICandleRepo
    {
        private readonly CandleContext _context;

        public CandleRepo(CandleContext context)
        {
            _context = context;
        }

        public async Task<Candle> CreateCandle(Candle candle)
        {
            _context.Add(candle);
            await _context.SaveChangesAsync();
            return candle;
        }

        public async Task<bool> DeleteCandle(Candle candle)
        {
            _context.Remove(candle);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Candle>> GetAllCandle()
        {
            var data = await _context.Candles.ToListAsync();
            return data;
        }

        public async Task<Candle> GetCandleById(int id)
        {
            var data = await _context.Candles.SingleOrDefaultAsync(x=> x.CandleId.Equals(id));
            return data;
        }

        public async Task<Candle> GetCandleByName(string? candle)
        {
            var data = await _context.Candles.SingleOrDefaultAsync(x => x.Name.Equals(candle));
            return data;
        }

        public async Task<Candle> UpdateCandle(Candle candle)
        {
            _context.Update(candle);
            await _context.SaveChangesAsync();
            return candle;
        }
    }
}
