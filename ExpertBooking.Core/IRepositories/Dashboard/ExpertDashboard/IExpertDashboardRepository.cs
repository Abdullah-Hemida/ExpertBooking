using ExpertBooking.Core.Entities;
using ExpertBooking.Core.Models;
using ExpertBooking.Core.Enums;

namespace ExpertBooking.Core.IRepositories.Dashboard.ExpertDashboard
{
    public interface IExpertDashboardRepository
    {
        Task<Expert?> GetExpertWithProfileAsync(Guid expertId);
        Task<Expert?> GetExpertAsync(Guid expertId);
        Task UpdateExpertAsync(Expert expert);

        Task<bool> DeleteCertificationAsync(Guid expertId, Guid docId);

        Task<List<Schedule>> GetScheduleAsync(Guid expertId);
        Task AddScheduleSlotAsync(Schedule slot);
        Task<bool> DeleteScheduleSlotAsync(Guid slotId);

        Task<List<Booking>> GetBookingsAsync(Guid expertId, BookingFilter filter);
        Task<Booking?> GetBookingByIdAsync(Guid bookingId);
        Task UpdateBookingAsync(Booking booking);
        Task<bool> UpdateBookingStatusAsync(Guid bookingId, BookingStatus status);
        Task<bool> AddNotesToBookingAsync(Guid bookingId, string notes);

        Task<List<Review>> GetReviewsAsync(Guid expertId);

        Task<int> GetTotalBookingsAsync(Guid expertId);
        Task<double> GetAverageRatingAsync(Guid expertId);
    }
}

