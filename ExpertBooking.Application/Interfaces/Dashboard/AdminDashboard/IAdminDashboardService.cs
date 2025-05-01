using ExpertBooking.Application.Helper;
using ExpertBooking.Contracts.DTOs.Dashboard.AdminDashboard;
using ExpertBooking.Core.Models;

namespace ExpertBooking.Application.Interfaces.Dashboard.AdminDashboard
{
    public interface IAdminDashboardService
    {
        Task<ServiceResponse<DashboardCountsDto>> GetDashboardCountsAsync();

        Task<ServiceResponse<List<ExpertDto>>> GetAllExpertsAsync(string? search, int page, int pageSize);
        Task<ServiceResponse<List<ClientDto>>> GetAllClientsAsync(string? search, int page, int pageSize);
        Task<ServiceResponse<List<AdminDto>>> GetAllAdminsAsync(string? search, int page, int pageSize);

        Task<ServiceResponse<ExpertDto>> GetExpertByIdAsync(Guid userId);
        Task<ServiceResponse<ClientDto>> GetClientByIdAsync(Guid userId);
        Task<ServiceResponse<AdminDto>> GetAdminByIdAsync(Guid userId);

        Task<ServiceResponse<bool>> ApproveExpertAsync(Guid userId);
        Task<ServiceResponse<bool>> UnapproveExpertAsync(Guid userId);

        Task<ServiceResponse<List<CategoryDto>>> GetAllCategoriesAsync();
        Task<ServiceResponse<CategoryDto>> GetCategoryByIdAsync(Guid categoryId);
        Task<ServiceResponse<CategoryDto>> CreateCategoryAsync(CategoryCreateDto categoryDto);
        Task<ServiceResponse<CategoryDto>> UpdateCategoryAsync(Guid categoryId, CategoryUpdateDto categoryDto);
        Task<ServiceResponse<bool>> DeleteCategoryAsync(Guid categoryId);

        Task<ServiceResponse<List<BookingDto>>> GetAllBookingsAsync(int page = 1, int pageSize = 10);
        Task<ServiceResponse<List<CategoryBookingStat>>> GetBookingStatsByCategoryAsync();
        Task<ServiceResponse<List<TopRatedExpert>>> GetTopRatedExpertsAsync(int topN = 5);
    }
}
