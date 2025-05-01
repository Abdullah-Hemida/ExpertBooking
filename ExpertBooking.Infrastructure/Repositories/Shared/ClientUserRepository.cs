using ExpertBooking.Core.Entities;
using ExpertBooking.Core.IRepositories.Shared;
using ExpertBooking.Infrastructure.Data;

namespace ExpertBooking.Infrastructure.Repositories.Shared
{
    public class ClientUserRepository : IClientUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ClientUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetByIdAsync(Guid userId)
        {
            return await _context.Clients.FindAsync(userId);
        }

        public async Task CreateAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }
    }
}
