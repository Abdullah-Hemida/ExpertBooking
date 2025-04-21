
using ExpertBooking.Core.Entities;

namespace ExpertBooking.Core.IRepositories.Website
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task RemoveAsync(RefreshToken refreshToken);
        Task SaveChangesAsync();
    }
}



