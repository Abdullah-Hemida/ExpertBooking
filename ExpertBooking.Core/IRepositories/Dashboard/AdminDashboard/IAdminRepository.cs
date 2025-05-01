using ExpertBooking.Core.Entities;

namespace ExpertBooking.Core.IRepositories.Dashboard.AdminDashboard
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
