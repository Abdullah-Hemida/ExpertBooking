using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ExpertBooking.Core.Entities
{
    public class Schedule
    {
        public Guid ScheduleId { get; set; } = Guid.NewGuid();

        public Guid? ExpertId { get; set; }
        public Expert? Expert { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(s => s.ScheduleId);

            builder.Property(s => s.DayOfWeek)
                .IsRequired();

            builder.Property(s => s.StartTime)
                .IsRequired();

            builder.Property(s => s.EndTime)
                .IsRequired();

            builder.HasOne(s => s.Expert)
                .WithMany(e => e.Schedules)
                .HasForeignKey(s => s.ExpertId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
