using ExpertBooking.Contracts.DTOs.Dashboard;
using ExpertBooking.Core.DTOsCore;
using ExpertBooking.Application.Helper;

namespace ExpertBooking.Application.Interfaces.Dashboard.Administration
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
        Task<ServiceResponse<List<CategoryBookingStatDto>>> GetBookingStatsByCategoryAsync();
        Task<ServiceResponse<List<TopRatedExpertDto>>> GetTopRatedExpertsAsync(int topN = 5);
    }
}
