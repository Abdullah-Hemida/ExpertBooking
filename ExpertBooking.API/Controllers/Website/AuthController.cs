using ExpertBooking.Contracts.DTOs.Website;
using ExpertBooking.Application.Interfaces.Website;
using Microsoft.AspNetCore.Mvc;

namespace ExpertBooking.API.Controllers.Website
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenDto dto)
        {
            var result = await _authService.RefreshTokenAsync(dto);
            return Ok(result);
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin(GoogleAuthDto dto)
        {
            var result = await _authService.RegisterWithGoogleAsync(dto);
            return Ok(result);
        }
    }

}




