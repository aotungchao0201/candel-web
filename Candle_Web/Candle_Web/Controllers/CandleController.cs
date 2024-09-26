using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Modals;
using Service.Services.Interface;

namespace Candle_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandleController : ControllerBase
    {
        private readonly ICandleService _candleService;

        public CandleController(ICandleService candleService)
        {
            _candleService = candleService;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCandle(int id, CandleDTO candle)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                bool isUpdated = await _candleService.updateCandle(id, candle);

                if (isUpdated)
                {
                    // Return a success response
                    return Ok();
                }
                else
                {
                    // Return a not found response if the service was not updated successfully
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Return a bad request response for any other exceptions
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoute(CandleDTO candle)
        {
            try
            {
                var data = await _candleService.createCandle(candle);
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

        [HttpDelete("delete-candle")]
        public async Task<IActionResult> DeleteCandleById(int id)
        {
            try
            {
                var result = await _candleService.deleteCandle(id);
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
        public async Task<IActionResult> GetAllCandle()
        {
            try
            {
                var result = await _candleService.GetAllcandleAscyn();
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
