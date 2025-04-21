using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ExpertBooking.Core.Enums;

namespace ExpertBooking.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName => $"{FirstName} {LastName}";
        public bool IsProfileCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? ProfileImageUrl { get; set; }

        // Role-related
        public UserType UserType { get; set; } = UserType.ApplicationUser;
        // Navigation
        public virtual Expert? ExpertProfile { get; set; }
        public virtual Client? ClientProfile { get; set; }
        public virtual Admin? AdminProfile { get; set; }
    }
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .HasMaxLength(50);

            builder.Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(u => u.ProfileImageUrl)
                .HasMaxLength(250);

            builder.HasOne(u => u.ExpertProfile)
                   .WithOne(e => e.User)
                   .HasForeignKey<Expert>(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.ClientProfile)
                   .WithOne(c => c.User)
                   .HasForeignKey<Client>(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.AdminProfile)
               .WithOne(c => c.User)
               .HasForeignKey<Admin>(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

