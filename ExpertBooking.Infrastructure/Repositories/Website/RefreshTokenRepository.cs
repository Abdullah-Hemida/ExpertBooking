using System;
using System.Linq;
using System.Threading.Tasks;
using ExpertBooking.Core.Entities;
using ExpertBooking.Core.IRepositories.Website;
using ExpertBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Infrastructure.Repositories.Website
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
        }

        public async Task RemoveAsync(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}



