using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Core.Entities
{
    public class Client
    {
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string? Preferences { get; set; }
        public string? Bio {  get; set; }

        // Navigation properties
        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.UserId);

            builder.HasOne(e => e.User)
                   .WithOne(u => u.ClientProfile)
                   .HasForeignKey<Client>(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Preferences)
                .HasMaxLength(500);

            builder.HasMany(c => c.Bookings)
                   .WithOne(b => b.Client)
                   .HasForeignKey(b => b.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Reviews)
                   .WithOne(r => r.Client)
                   .HasForeignKey(r => r.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}