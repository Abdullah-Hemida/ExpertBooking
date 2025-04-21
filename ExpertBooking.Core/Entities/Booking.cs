using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ExpertBooking.Core.Enums;

namespace ExpertBooking.Core.Entities
{
    public class Booking
    {
        public Guid BookingId { get; set; } = Guid.NewGuid();

        // Foreign keys and navigation properties
        public Guid? ExpertId { get; set; }
        public Expert? Expert { get; set; }

        public Guid? ClientId { get; set; }
        public Client? Client { get; set; }

        public DateTime ScheduledDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Notes { get; set; }

        // Optional one-to-one relationship with Payment
        public Payment Payment { get; set; }
        public Review Review { get; set; }


        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

    }
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.BookingId);

            builder.Property(b => b.ScheduledDateTime)
                .IsRequired();

            builder.Property(b => b.Duration)
                .IsRequired();

            builder.Property(b => b.Status)
                .IsRequired();

            builder.Property(b => b.Notes)
                .HasMaxLength(1000);

            builder.HasOne(b => b.Client)
                   .WithMany(c => c.Bookings)
                   .HasForeignKey(b => b.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Expert)
                   .WithMany()
                   .HasForeignKey(b => b.ExpertId)
                   .OnDelete(DeleteBehavior.Restrict);

            // One-to-one relationship with Payment
            builder.HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // many-to-one relationship with Category
            builder.HasOne(e => e.Category)
                   .WithMany(c => c.Bookings)
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
