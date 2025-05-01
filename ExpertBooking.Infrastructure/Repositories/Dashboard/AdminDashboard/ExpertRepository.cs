using ExpertBooking.Core.Entities;
using ExpertBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ExpertBooking.Core.IRepositories.Dashboard.AdminDashboard;

namespace ExpertBooking.Infrastructure.Repositories.Dashboard.AdminDashboard
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpertRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expert>> GetPagedAsync(string? search, int page, int pageSize)
        {
            var query = _context.Experts.Include(a => a.User).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => (e.User.FirstName + " " + e.User.LastName).ToLower().Contains(search.ToLower()));
            }
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Expert> GetByIdAsync(Guid userId)
        {
            return await _context.Experts.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Experts.CountAsync();
        }

        public async Task UpdateAsync(Expert expert)
        {
            _context.Experts.Update(expert);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Expert expert)
        {
            await _context.Experts.AddAsync(expert);
            await _context.SaveChangesAsync();
        }
    }
}
