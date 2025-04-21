
using ExpertBooking.Core.DTOsCore;

namespace ExpertBooking.Core.IRepositories.Dashboard.Administration
{
    public interface IReviewRepository
    {
        Task<List<TopRatedExpertDto>> GetTopRatedExpertsAsync(int topN);
        Task<int> CountAsync();
    }
}
