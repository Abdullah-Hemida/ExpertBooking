using ExpertBooking.Core.Entities;
using ExpertBooking.Core.Models;
namespace ExpertBooking.Core.IRepositories.Dashboard.AdminDashboard
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetPagedAsync(int page, int pageSize);
        Task<Booking> GetByIdAsync(Guid bookingId);
        Task<int> CountAsync();
        Task<List<CategoryBookingStat>> GetBookingStatsByCategoryAsync();
    }
}


