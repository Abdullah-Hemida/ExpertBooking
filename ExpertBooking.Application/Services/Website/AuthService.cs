using AutoMapper;
using ExpertBooking.Application.Interfaces.Website;
using ExpertBooking.Contracts.DTOs.Website;
using ExpertBooking.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExpertBooking.Application.Services.Website
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IGoogleAuthService _googleAuthService;

        public AuthService(UserManager<ApplicationUser> userManager,
                           ITokenService tokenService,
                           IGoogleAuthService googleAuthService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _googleAuthService = googleAuthService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, "Client"); // Default

            return await _tokenService.CreateJwtTokenAsync(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new UnauthorizedAccessException("Invalid credentials");

            return await _tokenService.CreateJwtTokenAsync(user);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var existingRefreshToken = await _tokenService.GetRefreshTokenAsync(dto.RefreshToken);
            if (existingRefreshToken == null || existingRefreshToken.Expires < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid or expired refresh token");

            var user = await _userManager.FindByIdAsync(existingRefreshToken.UserId.ToString());
            if (user == null)
                throw new UnauthorizedAccessException("User not found");

            // Optional: remove old refresh token
            await _tokenService.RemoveRefreshTokenAsync(existingRefreshToken);

            return await _tokenService.CreateJwtTokenAsync(user);
        }


        public async Task<AuthResponseDto> RegisterWithGoogleAsync(GoogleAuthDto dto)
        {
            var payload = await _googleAuthService.VerifyGoogleTokenAsync(dto.IdToken);
            if (payload == null)
                throw new UnauthorizedAccessException("Invalid Google token");

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = payload.Email,
                    UserName = payload.Email,
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "Client"); // Default
            }

            return await _tokenService.CreateJwtTokenAsync(user);
        }
    }
}








