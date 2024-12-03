using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Repository.Interface
{
    public interface IReViewRepo
    {
        public Task<List<Review>> GetAllReview();
        public Task<Review> CreateReview(Review data);
        public Task<Review> UpdateReview(Review data);
        public Task<bool> DeleteReview(Review data);

        public Task<Review> GetReviewById(int id);
        public Task<Review> GetReviewByName(string? data);
    }
}
