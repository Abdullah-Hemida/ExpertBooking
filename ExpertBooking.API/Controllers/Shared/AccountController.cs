using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpertBooking.Core.Enums;
using ExpertBooking.Application.Interfaces.Website;
using Microsoft.AspNetCore.Identity;
using ExpertBooking.Core.Entities;

namespace ExpertBooking.API.Controllers.Shared
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountUserService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(IAccountUserService accountService, ITokenService tokenService, UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("select-role")]
        public async Task<IActionResult> SelectRoleAsync([FromBody] SelectRoleDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            var userType = (UserType)Enum.Parse(typeof(UserType), dto.UserType.ToString());

            await _accountService.SelectRoleAsync(userId, userType);

            var tokens = await _tokenService.CreateJwtTokenAsync(userId);

            return Ok(tokens);
        }

        [Authorize(Roles = "Expert")]
        [HttpPost("complete-expert-profile")]
        public async Task<IActionResult> CompleteExpertProfileAsync([FromForm] ExpertProfileFormDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _accountService.CompleteExpertProfileAsync(userId!, dto);
            return Ok();
        }

        [Authorize(Roles = "Client")]
        [HttpPost("complete-client-profile")]
        public async Task<IActionResult> CompleteClientProfileAsync([FromForm] ClientProfileFormDto dto)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _accountService.CompleteClientProfileAsync(userId!, dto);
            return Ok();
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<UserProfileDto>> GetUserProfileAsync()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var profile = await _accountService.GetUserProfileAsync(userId!);
            return Ok(profile);
        }
    }
}







