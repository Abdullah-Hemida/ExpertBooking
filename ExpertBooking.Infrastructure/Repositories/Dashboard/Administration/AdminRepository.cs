
using ExpertBooking.Core.Entities;
using ExpertBooking.Core.IRepositories.Dashboard.Administration;
using ExpertBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace ExpertBooking.Infrastructure.Repositories.Dashboard.Administration
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Admin>> GetPagedAsync(string? search, int page, int pageSize)
        {
            var query = _context.Admins.Include(a => a.User).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => (a.User.FirstName + " " + a.User.LastName).ToLower().Contains(search.ToLower()));
            }
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Admin> GetByIdAsync(Guid userId)
        {
            return await _context.Admins.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Admins.CountAsync();
        }

        public async Task UpdateAsync(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }
    }
}
