using ExpertBooking.Application.Helper;
using ExpertBooking.Contracts.DTOs.Dashboard.AdminDashboard;
using ExpertBooking.Contracts.DTOs.Dashboard.ExpertDashboard;
using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Contracts.DTOs.Website;

namespace ExpertBooking.Application.Interfaces.Website
{
    public interface IWebsiteService
    {
        Task<ServiceResponse<List<ExpertCardDto>>> GetFeaturedExpertsAsync();
        Task<ServiceResponse<List<ExpertCardDto>>> SearchExpertsAsync(string? keyword, Guid? categoryId);
        Task<ServiceResponse<ExpertProfileDto>> GetExpertProfileAsync(Guid expertId);
        Task<ServiceResponse<List<ScheduleDto>>> GetExpertScheduleAsync(Guid expertId);
        Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<ServiceResponse<List<ExpertCardDto>>> GetExpertsByCategoryAsync(Guid categoryId);
        Task<ServiceResponse<List<ScheduleDto>>> GetAvailableSlotsAsync(Guid expertId);
        Task<ServiceResponse<bool>> CreateBookingAsync(Guid clientId, CreateBookingDto dto);
    }
}
