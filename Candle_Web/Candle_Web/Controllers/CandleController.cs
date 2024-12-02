using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Modals;
using Service.Modals.Request;
using Service.Services.Interface;
using System.Net;

namespace Candle_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandleController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;

        private readonly ICandleService _candleService;

        public CandleController(Cloudinary cloudinary, ICandleService candleService)
        {
            _cloudinary = cloudinary;
            _candleService = candleService;
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCandle(int id, [FromForm] CandleRequest candle)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Xử lý tải lên ảnh
                if (candle.ImgFile != null && candle.ImgFile.Length > 0)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(candle.ImgFile.FileName, candle.ImgFile.OpenReadStream()),
                        UseFilename = true,
                        UniqueFilename = true,
                        Overwrite = true
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.StatusCode != HttpStatusCode.OK)
                    {
                        return BadRequest("Image upload failed.");
                    }

                    // Lưu đường dẫn hình ảnh vào ImgUrl
                    candle.ImgUrl = uploadResult.SecureUrl.ToString();
                }

                // Gọi service để cập nhật candle
                bool isUpdated = await _candleService.updateCandle(id, candle);

                if (isUpdated)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateCandle([FromForm] CandleRequest candle)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Xử lý tải lên ảnh
                if (candle.ImgFile != null && candle.ImgFile.Length > 0)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(candle.ImgFile.FileName, candle.ImgFile.OpenReadStream()),
                        UseFilename = true,
                        UniqueFilename = true,
                        Overwrite = true
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.StatusCode != HttpStatusCode.OK)
                    {
                        return BadRequest("Image upload failed.");
                    }

                    // Lưu đường dẫn hình ảnh vào ImgUrl
                    candle.ImgUrl = uploadResult.SecureUrl.ToString();
                }
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

        [HttpDelete("delete-candle/{id}")]
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
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetCandleById(int id)
        {
            try
            {
                var result = await _candleService.GetByid(id);
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
        [HttpGet("get-by-category-id/{id}")]
        public async Task<IActionResult> GetCandleByCategoryId(int id)
        {
            try
            {
                var result = await _candleService.GetAllCandleByCategory(id);
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
