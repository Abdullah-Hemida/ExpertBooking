using ExpertBooking.Core.Entities;

namespace ExpertBooking.Core.IRepositories.Shared
{
    public interface IAccountUserRepository
    {
        Task<ApplicationUser?> GetByIdAsync(Guid userId);
        Task UpdateAsync(ApplicationUser user);
    }

}
