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
    public class ReviewRepo : IReViewRepo
    {
        private readonly candleContext _context;

        public ReviewRepo(candleContext context)
        {
            _context = context;
        }
        public async Task<Review> CreateReview(Review data)
        {
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteReview(Review data)
        {
            _context.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Review>> GetAllReview()
        {
            var data = await _context.Reviews.ToListAsync();
            return data;
        }

        public async Task<Review> GetReviewById(int id)
        {
            var data = await _context.Reviews.SingleOrDefaultAsync(x =>x.ReviewId.Equals(id));
            return data;
        }

        public Task<Review> GetReviewByName(string? data)
        {
            throw new NotImplementedException();
        }

        public async Task<Review> UpdateReview(Review data)
        {
            _context.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}
