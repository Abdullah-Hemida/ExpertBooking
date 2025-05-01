

namespace ExpertBooking.Contracts.DTOs.Dashboard.ExpertDashboard
{
    public class ExpertUpdateDto
    {
        public string JobTitle { get; set; }
        public string Bio { get; set; }
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        public Guid CategoryId { get; set; }
    }
    public class ExpertDashboardStatsDto
    {
        public int TotalBookings { get; set; }
        public double AverageRating { get; set; }
    }
    public class ScheduleDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Notes { get; set; }
    }

    public class BookingExpertDto
    {
        public Guid BookingId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

    public class ReviewExpertDto
    {
        public string ClientName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class FileUpload
    {
        public string FileName { get; set; } = string.Empty;
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public string ContentType { get; set; } = string.Empty;
    }
}
