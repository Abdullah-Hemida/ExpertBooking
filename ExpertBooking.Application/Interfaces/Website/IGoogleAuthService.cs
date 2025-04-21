using ExpertBooking.Contracts.DTOs.Website;


namespace ExpertBooking.Application.Interfaces.Website
{
    public interface IGoogleAuthService
    {
        Task<GooglePayload?> VerifyGoogleTokenAsync(string idToken);
    }
}


