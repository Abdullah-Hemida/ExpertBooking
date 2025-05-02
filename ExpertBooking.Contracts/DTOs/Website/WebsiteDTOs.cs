using ExpertBooking.Contracts.DTOs.Dashboard.AdminDashboard;

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
    public class ExpertCardDto
    {
        public Guid ExpertId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public decimal HourlyRate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
    public class ExpertSearchFilterDto
    {
        public string? Keyword { get; set; }
        public Guid? CategoryId { get; set; }
        public decimal? MinRate { get; set; }
        public decimal? MaxRate { get; set; }
        public int? MinExperience { get; set; }
        public double? MinRating { get; set; }
    }
    public class ExpertPublicProfileDto
    {
        public Guid ExpertId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? IntroductionVideoUrl { get; set; }
        public List<string> Certifications { get; set; } = new();
        public string CategoryName { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
    public class ScheduleSlotDto
    {
        public Guid ScheduleId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Notes { get; set; }
    }
    public class WebsiteHomeDto
    {
        public List<ExpertCardDto> FeaturedExperts { get; set; } = new();
        public List<CategoryDto> PopularCategories { get; set; } = new();
    }
    public class CreateBookingDto
    {
        public Guid ExpertId { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid CategoryId { get; set; }
        public string? Notes { get; set; }
    }

}



