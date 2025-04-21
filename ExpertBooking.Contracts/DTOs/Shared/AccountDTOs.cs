using Microsoft.AspNetCore.Http;
using ExpertBooking.Contracts.DTOs.Dashboard;

namespace ExpertBooking.Contracts.DTOs.Shared
{
    public class UserProfileDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; } = string.Empty;
        public UserTypeDTO UserType { get; set; }

        // Role-specific data
        public ExpertProfileDto? ExpertProfile { get; set; }
        public ClientProfileDto? ClientProfile { get; set; }
    }
    public class SelectRoleDto
    {
        public UserTypeDTO UserType { get; set; }
    }

    public enum UserTypeDTO
    {
        Client,
        Expert
    }
    public class ExpertProfileDto
    {
        public string JobTitle { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        public string IdentificationDocumentUrl { get; set; } = string.Empty;
        public string? IntroductionVideoUrl { get; set; }
        public ExperStatusDTO Status { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public List<ExpertDocumentDto>? Certifications { get; set; }
    }

    public class ExpertDocumentDto
    {
        public Guid ExpertDocumentId { get; set; }
        public Guid? ExpertId { get; set; } // Foreign key to Expert
        public string? FileUrl { get; set; } // Path to file storage
        public bool IsVerified { get; set; } = false;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }

    public class ClientProfileDto
    {
        public string PreferredField { get; set; } = string.Empty;
        public string AdditionalNotes { get; set; } = string.Empty;
    }
    public class ExpertProfileFormDto
    {
        public string FirstName {  get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        public Guid CategoryId { get; set; }

        public IFormFile? ProfileImage { get; set; }
        public IFormFile? IntroductionVideo { get; set; }
        public IFormFile? IdentificationDocument { get; set; }
        public List<IFormFile>? Certifications { get; set; }
    }
    public class ClientProfileFormDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Preferences{ get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public IFormFile? ProfileImage { get; set; }
    }
}




