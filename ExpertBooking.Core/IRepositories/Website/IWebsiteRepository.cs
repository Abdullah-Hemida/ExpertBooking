using ExpertBooking.Core.Entities;

namespace ExpertBooking.Core.IRepositories.Website
{
    public interface IWebsiteRepository
    {
        Task<List<Expert>> GetFeaturedExpertsAsync(int count);
        Task<List<Expert>> SearchExpertsAsync(string? keyword, Guid? categoryId);
        Task<Expert?> GetExpertByIdAsync(Guid expertId);
        Task<List<Schedule>> GetExpertScheduleAsync(Guid expertId);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<Expert>> GetExpertsByCategoryAsync(Guid categoryId);
        Task<List<Schedule>> GetAvailableSlotsAsync(Guid expertId);
        Task<bool> IsSlotAvailableAsync(Guid expertId, DateTime dateTime, TimeSpan duration);
        Task<bool> CreateBookingAsync(Booking booking);

    }

}
