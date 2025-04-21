using ExpertBooking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertBooking.Core.IRepositories.Dashboard.Administration
{
    public interface IAdminRepository
    {
        Task<List<Admin>> GetPagedAsync(string? search, int page, int pageSize);
        Task<Admin> GetByIdAsync(Guid userId);
        Task<int> CountAsync();
        Task UpdateAsync(Admin admin);
        Task AddAsync(Admin admin);
    }
}
