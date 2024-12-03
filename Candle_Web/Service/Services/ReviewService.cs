using AutoMapper;
using Model.Models;
using Repo.Repository;
using Repo.Repository.Interface;
using Service.Modals;
using Service.Modals.Request;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReViewRepo _review;
        private readonly IMapper _mapper;

        public ReviewService(IReViewRepo review, IMapper mapper)
        {
            _review = review;
            _mapper = mapper;
        }

        public async Task<ReviewRequest> Create(ReviewRequest candleDTO)
        {
            try
            {
                var map = _mapper.Map<Review>(candleDTO);
                var userCreate = await _review.CreateReview(map);
                var resutl = _mapper.Map<ReviewRequest>(userCreate);
                return resutl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var user = await _review.GetReviewById(id);
                if (user == null)
                {
                    throw new Exception($"Review {id} does not exist");
                }

                await _review.DeleteReview(user);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReviewDTO>> GetAll()
        {
            try
            {

                var data = await _review.GetAllReview();
                var map = _mapper.Map<List<ReviewDTO>>(data);
                return map;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Update(int id, ReviewRequest dto)
        {
            throw new NotImplementedException();
        }
    }
}
