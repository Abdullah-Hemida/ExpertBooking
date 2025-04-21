using ExpertBooking.Core.DTOsCore;
using ExpertBooking.Core.IRepositories.Dashboard.Administration;
using ExpertBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Infrastructure.Repositories.Dashboard.Administration
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TopRatedExpertDto>> GetTopRatedExpertsAsync(int topN)
        {
            return await _context.Reviews
                .Where(r => r.ExpertId != null)
                .GroupBy(r => r.ExpertId)
                .OrderByDescending(g => g.Average(r => r.Rating))
                .Take(topN)
                .Join(_context.Experts,
                      g => g.Key,
                      e => e.User.Id,
                      (g, e) => new TopRatedExpertDto
                      {
                          ExpertId = e.User.Id,
                          FullName = e.User.FullName,
                          JobTitle = e.JobTitle,
                          AverageRating = g.Average(r => r.Rating),
                          TotalReviews = g.Count()
                      })
                .ToListAsync();
        }
        public async Task<int> CountAsync()
        {
            return await _context.Reviews.CountAsync();
        }
    }
}

