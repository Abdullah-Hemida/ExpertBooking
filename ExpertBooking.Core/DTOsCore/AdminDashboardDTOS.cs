
namespace ExpertBooking.Core.DTOsCore
{
    public class CategoryBookingStatDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BookingCount { get; set; }
    }
    public class TopRatedExpertDto
    {
        public Guid ExpertId { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }
}
