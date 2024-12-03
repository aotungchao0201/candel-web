using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repo.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repository
{
    public class CateRepo : ICateRepo
    {
        private readonly candleContext _context;

        public CateRepo(candleContext context)
        {
            _context = context;
        }
        public async Task<Category> Create(Category data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> Delete(Category data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Category>> GetALL()
        {
            var data = await _context.Categories.ToListAsync();
            return data;
        }

        public async Task<List<Candle>> GetCandleByCategoryId(int id)
        {
            var candles = await _context.Candles
                           .Where(c => c.CategoryId == id)
                           .ToListAsync();

            return candles;
        }

        

        public async Task<Category> GetCateById(int id)
        {
            var data = await _context.Categories.SingleOrDefaultAsync(x => x.CategoryId.Equals(id));
            return data;
        }
    }
}
