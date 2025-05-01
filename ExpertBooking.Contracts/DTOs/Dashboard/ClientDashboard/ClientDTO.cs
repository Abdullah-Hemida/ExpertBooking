
namespace ExpertBooking.Contracts.DTOs.Dashboard.ClientDashboard
{
    public class ClientUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Preferences { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
    }
    public class ClientDashboardStatsDto
    {
        public int TotalBookings { get; set; }
        public int ReviewsCount { get; set; }
    }
    public class BookingClientDto
    {
        public Guid BookingId { get; set; }
        public string ExpertName { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
    public class ReviewClientDto
    {
        public Guid ReviewId { get; set; }
        public string ExpertName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
    public class ReviewClientCreateDto
    {
        public Guid ExpertId { get; set; }
        public Guid BookingId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
