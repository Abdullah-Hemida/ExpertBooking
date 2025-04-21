
using ExpertBooking.Core.Entities;

namespace ExpertBooking.Core.IRepositories.Dashboard.Administration
{
    public interface ICategoryRepository
    {
            Task<List<Category>> GetAllAsync();
            Task<Category> GetByIdAsync(Guid categoryId);
            Task AddAsync(Category category);
            Task UpdateAsync(Category category);
            Task DeleteAsync(Category category);
    }
}


