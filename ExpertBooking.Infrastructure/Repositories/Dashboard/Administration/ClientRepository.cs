using ExpertBooking.Core.Entities;
using ExpertBooking.Infrastructure.Data;
using ExpertBooking.Core.IRepositories.Dashboard.Administration;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Infrastructure.Repositories.Dashboard.Administration
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetPagedAsync(string? search, int page, int pageSize)
        {
            var query = _context.Clients.Include(a => a.User).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => (c.User.FirstName + " " + c.User.LastName).ToLower().Contains(search.ToLower()));
            }
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Client> GetByIdAsync(Guid userId)
        {
            return await _context.Clients.Include(a => a.User).FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Clients.CountAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }
    }
}
