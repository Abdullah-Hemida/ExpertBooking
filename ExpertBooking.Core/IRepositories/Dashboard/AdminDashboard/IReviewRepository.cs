using ExpertBooking.Core.Models;
namespace ExpertBooking.Core.IRepositories.Dashboard.AdminDashboard
{
    public interface IReviewRepository
    {
        Task<List<TopRatedExpert>> GetTopRatedExpertsAsync(int topN);
        Task<int> CountAsync();
    }
}
