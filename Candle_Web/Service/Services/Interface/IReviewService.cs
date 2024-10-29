using Service.Modals.Request;
using Service.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interface
{
    public interface IReviewService
    {
        Task<List<ReviewDTO>> GetAll();

        Task<ReviewRequest> Create(ReviewRequest candleDTO);
        Task<bool> Update(int id, ReviewRequest dto);
        Task<bool> Delete(int id);
    }
}
