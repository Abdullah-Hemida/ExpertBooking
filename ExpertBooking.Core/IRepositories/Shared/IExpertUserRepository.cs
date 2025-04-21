
using ExpertBooking.Core.Entities;

namespace ExpertBooking.Core.IRepositories.Shared
{
    public interface IExpertUserRepository
    {
        Task<Expert?> GetByIdAsync(Guid userId);
        Task<Expert?> GetByIdWithCategoryAndDocumentsAsync(Guid userId);
        Task CreateAsync(Expert expert);
    }
}


