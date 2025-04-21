using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ExpertBooking.Core.Entities
{
    public class ExpertDocument
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? ExpertId { get; set; } // Foreign key to Expert
        public string? FileUrl { get; set; } // Path to file storage
        public bool IsVerified { get; set; } = false;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public Expert? Expert { get; set; } // Navigation property
    }

    public class ExpertDocumentConfiguration : IEntityTypeConfiguration<ExpertDocument>
    {
        public void Configure(EntityTypeBuilder<ExpertDocument> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.FileUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(d => d.Expert)
                .WithMany(e => e.ExpertDocuments)
                .HasForeignKey(d => d.ExpertId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(d => d.IsVerified)
                   .IsRequired()
                   .HasDefaultValue(false);

        }
    }
}


