using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Core.Entities
{
    public class Admin
    {
        public Guid UserId { get; set; } 
        public ApplicationUser? User { get; set; }
        public string? AdminRoleInfo { get; set; }
    }

    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.HasOne(e => e.User)
                   .WithOne(u => u.AdminProfile)
                   .HasForeignKey<Admin>(e => e.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.AdminRoleInfo)
                .HasMaxLength(200);
        }
    }
}
