using ExpertBooking.Contracts.DTOs.Shared;
using ExpertBooking.Core.Enums;
namespace ExpertBooking.Application.Interfaces.Shared
{
    public interface IAccountUserService
    {
        Task SelectRoleAsync(Guid userId, UserType userType);
        Task CompleteExpertProfileAsync(Guid userId, ExpertProfileFormDto dto);
        Task CompleteClientProfileAsync(Guid userId, ClientProfileFormDto dto);
        Task<UserProfileDto> GetUserProfileAsync(Guid userId);
    }
}



