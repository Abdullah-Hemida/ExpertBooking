using ExpertBooking.Core.Entities;
using ExpertBooking.Core.DTOsCore;
namespace ExpertBooking.Core.IRepositories.Dashboard.Administration
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetPagedAsync(int page, int pageSize);
        Task<Booking> GetByIdAsync(Guid bookingId);
        Task<int> CountAsync();
        Task<List<CategoryBookingStatDto>> GetBookingStatsByCategoryAsync();
    }
}


