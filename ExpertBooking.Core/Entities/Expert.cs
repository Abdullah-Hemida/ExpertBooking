using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ExpertBooking.Core.Enums;

namespace ExpertBooking.Core.Entities
{
    public class Expert
    {
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string JobTitle { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal HourlyRate { get; set; }
        public string IdentificationDocumentUrl { get; set; } = string.Empty;
        public string? IntroductionVideoUrl { get; set; }
        public ExpertStatus Status { get; set; } = ExpertStatus.Pending;

        // New One-to-Many relationship
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<ExpertDocument>? ExpertDocuments { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }
    public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
    {
        public void Configure(EntityTypeBuilder<Expert> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.HasOne(e => e.User)
                   .WithOne(u => u.ExpertProfile)
                   .HasForeignKey<Expert>(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.JobTitle)
                .HasMaxLength(500)
                 .IsRequired();

            builder.Property(e => e.Bio)
                .HasMaxLength(1000)
                 .IsRequired();

            builder.Property(e => e.ExperienceYears)
                .IsRequired();

            builder.Property(e => e.HourlyRate)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.IdentificationDocumentUrl)
                .IsRequired();

            // Relationships
            builder.HasOne(e => e.Category)
                   .WithMany(c => c.Experts)
                   .HasForeignKey(e => e.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.ExpertDocuments)
                   .WithOne(d => d.Expert)
                   .HasForeignKey(d => d.ExpertId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Bookings)
                .WithOne(b => b.Expert)
                .HasForeignKey(b => b.ExpertId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Reviews)
                .WithOne(r => r.Expert)
                .HasForeignKey(r => r.ExpertId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Schedules)
                .WithOne(s => s.Expert)
                .HasForeignKey(s => s.ExpertId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
