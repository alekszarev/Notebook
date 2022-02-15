using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Data.Models.Calendar;

namespace Notebook.Data.ModelsConfig
{
    public class DayConfig : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.HasKey(x => x.DayId);

            //      builder.HasOne(x => x.Calendar)
            //.WithMany(x => x.Days)
            //.HasForeignKey(x => x.CalendarId)
            //.OnDelete(DeleteBehavior.NoAction);



            builder.HasOne(x => x.Month)
                .WithMany(x => x.Days)
                .HasForeignKey(x => x.MonthId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}