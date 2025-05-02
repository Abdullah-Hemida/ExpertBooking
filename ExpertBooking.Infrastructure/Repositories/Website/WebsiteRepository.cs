using ExpertBooking.Core.Entities;
using ExpertBooking.Core.Enums;
using ExpertBooking.Core.IRepositories.Website;
using ExpertBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Infrastructure.Repositories.Website
{
    public class WebsiteRepository : IWebsiteRepository
    {
        private readonly ApplicationDbContext _context;

        public WebsiteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expert>> GetFeaturedExpertsAsync(int count)
        {
            return await _context.Experts
                .Include(e => e.User)
                .Include(e => e.Category)
                .Include(e => e.ExpertDocuments)
                .OrderByDescending(e => e.Reviews.Average(r => (double?)r.Rating) ?? 0)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Expert>> SearchExpertsAsync(string? keyword, Guid? categoryId)
        {
            var query = _context.Experts
                .Include(e => e.User)
                .Include(e => e.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(e =>
                    e.User.FirstName.Contains(keyword) ||
                    e.User.LastName.Contains(keyword) ||
                    e.JobTitle.Contains(keyword) ||
                    e.Bio.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(e => e.CategoryId == categoryId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Expert?> GetExpertByIdAsync(Guid expertId)
        {
            return await _context.Experts
                .Include(e => e.User)
                .Include(e => e.Category)
                .Include(e => e.ExpertDocuments)
                .FirstOrDefaultAsync(e => e.UserId == expertId);
        }

        public async Task<List<Schedule>> GetExpertScheduleAsync(Guid expertId)
        {
            return await _context.Schedules
                .Where(s => s.ExpertId == expertId && s.StartTime > DateTime.UtcNow)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
        public async Task<List<Expert>> GetExpertsByCategoryAsync(Guid categoryId)
        {
            return await _context.Experts
                .Include(e => e.User)
                .Include(e => e.Category)
                .Include(e => e.Reviews)
                .Where(e => e.CategoryId == categoryId && e.Status == ExpertStatus.Approved)
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetAvailableSlotsAsync(Guid expertId)
        {
            var now = DateTime.UtcNow;
            return await _context.Schedules
                .Where(s => s.ExpertId == expertId && s.StartTime > now)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }
        public async Task<bool> IsSlotAvailableAsync(Guid expertId, DateTime dateTime, TimeSpan duration)
        {
            return await _context.Schedules.AnyAsync(s =>
                s.ExpertId == expertId &&
                s.StartTime <= dateTime &&
                s.EndTime >= dateTime.Add(duration));
        }

        public async Task<bool> CreateBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}
