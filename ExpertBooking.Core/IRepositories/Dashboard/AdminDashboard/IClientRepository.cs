using ExpertBooking.Core.Entities;
using System;

namespace ExpertBooking.Core.IRepositories.Dashboard.AdminDashboard
{
    public interface IClientRepository
    {
        Task<List<Client>> GetPagedAsync(string? search, int page, int pageSize);
        Task<Client> GetByIdAsync(Guid userId);
        Task<int> CountAsync();
        Task UpdateAsync(Client client);
        Task AddAsync(Client client);
    }
}
