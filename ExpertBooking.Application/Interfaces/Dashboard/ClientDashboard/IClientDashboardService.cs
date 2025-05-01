
using ExpertBooking.Contracts.DTOs.Dashboard.ClientDashboard;
using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Application.Helper;
using ExpertBooking.Core.Models;

namespace ExpertBooking.Application.Interfaces.Dashboard.ClientDashboard
{
    public interface IClientDashboardService
    {
        Task<ServiceResponse<ClientProfileDto>> GetClientProfileAsync(Guid clientId);
        Task<ServiceResponse<bool>> UpdateClientProfileAsync(Guid clientId, ClientUpdateDto dto);

        Task<ServiceResponse<List<BookingClientDto>>> GetMyBookingsAsync(Guid clientId, BookingFilter filter);
        Task<ServiceResponse<bool>> CancelBookingAsync(Guid bookingId);

        Task<ServiceResponse<List<ReviewClientDto>>> GetMyReviewsAsync(Guid clientId);
        Task<ServiceResponse<bool>> AddReviewAsync(Guid clientId, ReviewClientCreateDto dto);

        Task<ServiceResponse<ClientDashboardStatsDto>> GetClientStatsAsync(Guid clientId);
    }
}


