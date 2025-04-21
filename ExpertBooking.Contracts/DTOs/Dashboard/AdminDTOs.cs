
namespace ExpertBooking.Contracts.DTOs.Dashboard
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsProfileCompleted { get; set; }
    }
    public class ExpertDto : UserDto
    {
        public string JobTitle {  get; set; } = string.Empty; 
        public string Bio { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }       
        public ExperStatusDTO Status { get; set; }
        public bool IsApproved => Status == ExperStatusDTO.Approved;
    }
    public enum ExperStatusDTO
    {
        Pending,
        Approved,
        Unapproved
    }
    public class ClientDto : UserDto
    {
        public string Preferences { get; set; } = string.Empty;
        public int BookingsNum { get; set; }
    }

    public class AdminDto : UserDto 
    {
        public string? AdminRoleInfo { get; set; }
    }
    public class DashboardCountsDto
    {
        public int TotalExperts { get; set; }
        public int TotalClients { get; set; }
        public int TotalBookings { get; set; }
        public int TotalCategories { get; set; }
    }

    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class BookingDto
    {
        public string ExpertName { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public BookingStatus Status { get; set; }
        public string? Notes { get; set; }
    }
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }
    public class PaginatedResultDto<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
