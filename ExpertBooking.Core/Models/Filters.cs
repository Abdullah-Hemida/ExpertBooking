using ExpertBooking.Core.Enums;

namespace ExpertBooking.Core.Models
{
    public class BookingFilter
    {
        public BookingStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class TopRatedExpert
    {
        public Guid ExpertId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class CategoryBookingStat
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BookingCount { get; set; }
    }
}
