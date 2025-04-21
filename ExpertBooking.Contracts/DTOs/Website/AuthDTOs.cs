namespace ExpertBooking.Contracts.DTOs.Website
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class RefreshTokenDto
    {
        public string RefreshToken { get; set; }
    }

    public class GoogleAuthDto
    {
        public string IdToken { get; set; }
    }

    public class GooglePayload
    {
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Picture { get; set; } = default!;
        public string Provider { get; set; } = "Google";
    }

}



