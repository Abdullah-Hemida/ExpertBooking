using ExpertBooking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertBooking.Core.IRepositories.Dashboard.Administration
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
