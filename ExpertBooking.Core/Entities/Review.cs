using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Core.Entities
{
    public class Review
    {
        public Guid ReviewId { get; set; } = Guid.NewGuid();

        public Guid BookingId { get; set; }
        public Booking? Booking { get; set; }
        public Guid? ExpertId { get; set; }
        public Expert? Expert { get; set; }

        public Guid? ClientId { get; set; }
        public Client? Client { get; set; }

        public int Rating { get; set; }  // e.g., 1-5 scale
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.ReviewId);

            builder.Property(r => r.Rating)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(1000);

            builder.HasOne(r => r.Booking)
                   .WithOne(b => b.Review)
                   .HasForeignKey<Review>(r => r.BookingId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Client)
                   .WithMany(c => c.Reviews)
                   .HasForeignKey(r => r.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Expert)
                   .WithMany()
                   .HasForeignKey(r => r.ExpertId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
