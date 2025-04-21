using ExpertBooking.Application.Interfaces.Website;
using ExpertBooking.Contracts.DTOs.Website;
using Google.Apis.Auth;
using System.Threading.Tasks;

namespace ExpertBooking.Application.Services.Website
{
    public class GoogleAuthService : IGoogleAuthService
    {
        public async Task<GooglePayload?> VerifyGoogleTokenAsync(string idToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);

                return new GooglePayload
                {
                    Email = payload.Email,
                    FirstName = payload.GivenName ?? "",
                    LastName = payload.FamilyName ?? "",
                    Picture = payload.Picture ?? ""
                };
            }
            catch
            {
                return null;
            }
        }
    }

}



