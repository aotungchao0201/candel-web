using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Modals.Request;
using System.Net;
using Service.Services.Interface;

namespace Candle_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICateService _cate;

        public CategoryController(ICateService cate)
        {
            _cate = cate;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCate([FromForm] CateRequest candle)
        {
            try
            {
               
                var data = await _cate.Create(candle);
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

        [HttpDelete("delete-cate/{id}")]
        public async Task<IActionResult> DeleteCandleById(int id)
        {
            try
            {
                var result = await _cate.Delete(id);
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
                var result = await _cate.GetALL();
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
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetAllCandle(int id)
        {
            try
            {
                var result = await _cate.GetCandleByCategoryId(id);
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
