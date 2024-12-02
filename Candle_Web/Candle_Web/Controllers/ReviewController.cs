using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Modals.Request;
using Service.Services.Interface;

namespace Candle_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewcontroller;

        public ReviewController(IReviewService reviewcontroller)
        {
            _reviewcontroller = reviewcontroller;
        }

        [HttpPost("create")]
        public async Task<IActionResult> createReview(ReviewRequest candle)
        {
            try
            {
                var data = await _reviewcontroller.Create(candle);
                if (data == null)
                {
                    return BadRequest();
                }
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }

        }

        [HttpDelete("delete-review/{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                var result = await _reviewcontroller.Delete(id);
                if (result)
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReview()
        {
            try
            {
                var result = await _reviewcontroller.GetAll();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}

