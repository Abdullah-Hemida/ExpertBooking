using ExpertBooking.Core.Entities;

namespace ExpertBooking.Core.IRepositories.Dashboard.AdminDashboard
{
    public interface IExpertRepository
    {
        Task<List<Expert>> GetPagedAsync(string? search, int page, int pageSize);
        Task<Expert> GetByIdAsync(Guid userId);
        Task<int> CountAsync();
        Task UpdateAsync(Expert expert);
        Task AddAsync(Expert expert);
    }
}
