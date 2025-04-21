using ExpertBooking.Core.Entities;
using ExpertBooking.Core.IRepositories.Dashboard.Administration;
using ExpertBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ExpertBooking.Core.DTOsCore;

namespace ExpertBooking.Infrastructure.Repositories.Dashboard.Administration
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Bookings
                .Include(b => b.Expert)
                .Include(b => b.Client)
                .Include(b => b.Category)
                .OrderByDescending(b => b.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Booking> GetByIdAsync(Guid bookingId)
        {
            return await _context.Bookings
                .Include(b => b.Expert)
                .Include(b => b.Client)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Bookings.CountAsync();
        }

        public async Task<List<CategoryBookingStatDto>> GetBookingStatsByCategoryAsync()
        {
            return await _context.Bookings
                .Include(b => b.Category)
                .GroupBy(b => new { b.CategoryId, b.Category.Name })
                .Select(g => new CategoryBookingStatDto
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.Name,
                    BookingCount = g.Count()
                })
                .ToListAsync();
        }
    }
}


