using ExpertBooking.Core.Entities;
using ExpertBooking.Core.Models;

namespace ExpertBooking.Core.IRepositories.Dashboard.ClientDashboard
{
    public interface IClientDashboardRepository
    {
        Task<Client?> GetClientWithProfileAsync(Guid clientId);
        Task<bool> UpdateClientProfileAsync(Client client); 

        Task<List<Booking>> GetBookingsAsync(Guid clientId, BookingFilter filter); 
        Task<bool> CancelBookingAsync(Guid bookingId);

        Task<List<Review>> GetMyReviewsAsync(Guid clientId);
        Task<bool> AddReviewAsync(Review review); 

        Task<(int TotalBookings, int ReviewsCount)> GetClientStatsAsync(Guid clientId);
    }
}


