using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Data.Models.Calendar;

namespace Notebook.Data.ModelsConfig
{
    public class YearConfig : IEntityTypeConfiguration<Year>
    {
        public void Configure(EntityTypeBuilder<Year> builder)
        {
            builder.HasKey(x => x.YearId);

            builder.HasOne(x => x.Calendar)
                .WithMany(x => x.Years)
                .HasForeignKey(x => x.CalendarId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}