using ExpertBooking.Contracts.DTOs.Website;
using System.Threading.Tasks;

namespace ExpertBooking.Application.Interfaces.Website
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
        Task<AuthResponseDto> RegisterWithGoogleAsync(GoogleAuthDto dto);
    }
}



