using ExpertBooking.Contracts.DTOs.Website;
using System.Threading.Tasks;
using ExpertBooking.Core.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace ExpertBooking.Application.Interfaces.Website
{
    public interface ITokenService
    {
        Task<AuthResponseDto> CreateJwtTokenAsync(ApplicationUser user);
        Task<AuthResponseDto> CreateJwtTokenAsync(Guid userId);
        Task<RefreshToken> CreateRefreshTokenAsync(Guid userId,string createdByIp);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        Task RemoveRefreshTokenAsync(RefreshToken token);
    }
}




