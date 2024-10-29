using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Modals.Respond;
using Service.Services;
using Service.Services.Interface;

namespace Candle_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _userService;
        private readonly TokenService _tokenService;

        public AuthenController(IAuthenService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRespond request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.PasswordHash))
            {
                return BadRequest("Invalid Email or password");
            }

            var user = await _userService.Login(request.Email, request.PasswordHash);
            if (user == null)
            {
                return NotFound("User not found");
            }           
            // Tạo token
            var token = _tokenService.GenerateToken(user.Username, user.RoleId);
            return Ok(new
            {
                Success = true,
                Message = "Authenticate success",
                Token = token
            });
        }
    }
}
