using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Data.Models.Calendar;

namespace Notebook.Data.ModelsConfig
{
    public class HourConfig : IEntityTypeConfiguration<Hour>
    {
        public void Configure(EntityTypeBuilder<Hour> builder)
        {
            builder.HasKey(x => x.HourId);

            builder.HasOne(x=>x.Appointment)
                .WithOne(x=>x.Hour)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Day)
                .WithMany(x => x.Hours)
                .HasForeignKey(x => x.DayId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
