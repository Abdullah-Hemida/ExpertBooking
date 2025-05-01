using ExpertBooking.Core.Entities;
using ExpertBooking.Core.Enums;
using ExpertBooking.Core.IRepositories.Dashboard.ClientDashboard;
using ExpertBooking.Core.Models;
using ExpertBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Infrastructure.Repositories.Dashboard.ClientDashboard
{
    public class ClientDashboardRepository : IClientDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientDashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetClientWithProfileAsync(Guid clientId)
        {
            return await _context.Clients
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.UserId == clientId);
        }

        public async Task<bool> UpdateClientProfileAsync(Client client)
        {
            _context.Clients.Update(client);
            _context.Users.Update(client.User!);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Booking>> GetBookingsAsync(Guid clientId, BookingFilter filter)
        {
            var query = _context.Bookings
                .Include(b => b.Expert)
                .ThenInclude(e => e.User)
                .Where(b => b.ClientId == clientId);

            if (filter.Status.HasValue)
                query = query.Where(b => b.Status == filter.Status.Value);

            if (filter.FromDate.HasValue)
                query = query.Where(b => b.ScheduledDateTime >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(b => b.ScheduledDateTime <= filter.ToDate.Value);

            return await query.OrderByDescending(b => b.ScheduledDateTime).ToListAsync();
        }

        public async Task<bool> CancelBookingAsync(Guid bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null) return false;

            booking.Status = BookingStatus.Cancelled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Review>> GetMyReviewsAsync(Guid clientId)
        {
            return await _context.Reviews
                .Where(r => r.ClientId == clientId)
                .Include(r => r.Expert)
                .ThenInclude(e => e.User)
                .ToListAsync();
        }

        public async Task<bool> AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(int TotalBookings, int ReviewsCount)> GetClientStatsAsync(Guid clientId)
        {
            var totalBookings = await _context.Bookings.CountAsync(b => b.ClientId == clientId);
            var reviewsCount = await _context.Reviews.CountAsync(r => r.ClientId == clientId);
            return (totalBookings, reviewsCount);
        }
    }
}


