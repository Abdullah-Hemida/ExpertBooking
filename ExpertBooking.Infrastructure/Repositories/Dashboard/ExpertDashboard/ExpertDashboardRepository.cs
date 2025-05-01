using ExpertBooking.Core.Entities;
using ExpertBooking.Core.IRepositories.Dashboard.ExpertDashboard;
using Microsoft.EntityFrameworkCore;
using ExpertBooking.Infrastructure.Data;
using ExpertBooking.Core.Enums;
using ExpertBooking.Core.Models;

namespace ExpertBooking.Infrastructure.Repositories.Dashboard.ExpertDashboard
{
    public class ExpertDashboardRepository : IExpertDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpertDashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Expert?> GetExpertWithProfileAsync(Guid expertId)
        {
            return await _context.Experts
                .Include(e => e.User)
                .Include(e => e.Category)
                .Include(e => e.ExpertDocuments)
                .FirstOrDefaultAsync(e => e.UserId == expertId);
        }

        public async Task<Expert?> GetExpertAsync(Guid expertId)
        {
            return await _context.Experts.FirstOrDefaultAsync(e => e.UserId == expertId);
        }

        public async Task UpdateExpertAsync(Expert expert)
        {
            _context.Experts.Update(expert);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCertificationAsync(Guid expertId, Guid docId)
        {
            var doc = await _context.ExpertDocuments
                .FirstOrDefaultAsync(d => d.ExpertId == expertId && d.Id == docId);
            if (doc == null) return false;

            _context.ExpertDocuments.Remove(doc);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Schedule>> GetScheduleAsync(Guid expertId)
        {
            return await _context.Schedules
                .Where(s => s.ExpertId == expertId)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task AddScheduleSlotAsync(Schedule slot)
        {
            await _context.Schedules.AddAsync(slot);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteScheduleSlotAsync(Guid slotId)
        {
            var slot = await _context.Schedules.FindAsync(slotId);
            if (slot == null) return false;

            _context.Schedules.Remove(slot);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Booking>> GetBookingsAsync(Guid expertId, BookingFilter filter)
        {
            var query = _context.Bookings
                .Include(b => b.Client)
                .Where(b => b.ExpertId == expertId);

            if (filter.Status.HasValue)
                query = query.Where(b => b.Status == filter.Status.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(b => b.ScheduledDateTime >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(b => b.ScheduledDateTime <= filter.ToDate.Value);

            return await query.OrderByDescending(b => b.ScheduledDateTime).ToListAsync();
        }

        public async Task<Booking?> GetBookingByIdAsync(Guid bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Client)
                .Include(b => b.Expert)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateBookingStatusAsync(Guid bookingId, BookingStatus status)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddNotesToBookingAsync(Guid bookingId, string notes)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Notes = notes;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Review>> GetReviewsAsync(Guid expertId)
        {
            return await _context.Reviews
                .Where(r => r.ExpertId == expertId)
                .Include(r => r.Client)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<int> GetTotalBookingsAsync(Guid expertId)
        {
            return await _context.Bookings.CountAsync(b => b.ExpertId == expertId);
        }

        public async Task<double> GetAverageRatingAsync(Guid expertId)
        {
            return await _context.Reviews
                .Where(r => r.ExpertId == expertId)
                .AverageAsync(r => (double?)r.Rating) ?? 0;
        }
    }
}

