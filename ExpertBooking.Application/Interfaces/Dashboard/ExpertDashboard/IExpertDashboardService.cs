using ExpertBooking.Application.Helper;
using ExpertBooking.Contracts.DTOs.Dashboard.ExpertDashboard;
using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Core.Models;
using Microsoft.AspNetCore.Http;

namespace ExpertBooking.Application.Interfaces.Dashboard.ExpertDashboard
{
    public interface IExpertDashboardService
    {
        // Profile Management
        Task<ServiceResponse<ExpertProfileDto>> GetExpertProfileAsync(Guid userId);
        Task<ServiceResponse<bool>> UpdateExpertProfileAsync(Guid userId, ExpertUpdateDto dto);
        Task<ServiceResponse<string>> UploadIntroductionVideoAsync(Guid userId, IFormFile video);
        Task<ServiceResponse<List<string>>> UploadCertificationAsync(Guid userId, List<IFormFile> certifications);
        Task<ServiceResponse<bool>> DeleteCertificationAsync(Guid expertId, Guid docId);

        // Schedule Management
        Task<ServiceResponse<List<ScheduleDto>>> GetScheduleAsync(Guid expertId);
        Task<ServiceResponse<bool>> AddScheduleSlotAsync(Guid expertId, ScheduleDto dto);
        Task<ServiceResponse<bool>> DeleteScheduleSlotAsync(Guid slotId);

        // Booking Management
        Task<ServiceResponse<List<BookingExpertDto>>> GetBookingsAsync(Guid expertId, BookingFilter filter);
        Task<ServiceResponse<bool>> ConfirmBookingAsync(Guid bookingId);
        Task<ServiceResponse<bool>> RejectBookingAsync(Guid bookingId);
        Task<ServiceResponse<bool>> AddNotesToBookingAsync(Guid bookingId, string notes);

        // Reviews
        Task<ServiceResponse<List<ReviewExpertDto>>> GetReviewsAsync(Guid expertId);

        // Statistics
        Task<ServiceResponse<ExpertDashboardStatsDto>> GetExpertDashboardStatsAsync(Guid expertId);
    }
}

