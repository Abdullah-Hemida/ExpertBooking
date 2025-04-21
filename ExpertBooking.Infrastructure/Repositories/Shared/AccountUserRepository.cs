using ExpertBooking.Core.Entities;
using ExpertBooking.Core.IRepositories.Shared;
using ExpertBooking.Infrastructure.Data;

namespace ExpertBooking.Infrastructure.Repositories.Shared
{
    public class AccountUserRepository : IAccountUserRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task UpdateAsync(ApplicationUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
