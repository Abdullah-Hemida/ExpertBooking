
using ExpertBooking.Core.Entities;

namespace ExpertBooking.Core.IRepositories.Shared
{
    public interface IClientUserRepository
    {
        Task<Client?> GetByIdAsync(Guid userId);
        Task CreateAsync(Client client);
    }
}
