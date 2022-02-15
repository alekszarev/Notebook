using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Data.Models.Calendar;

namespace Notebook.Data.ModelsConfig
{
    public class MonthConfig : IEntityTypeConfiguration<Month>
    {
        public void Configure(EntityTypeBuilder<Month> builder)
        {
            builder.HasKey(x => x.MonthId);

            builder.HasOne(x => x.Year)
                .WithMany(x => x.Months)
                .HasForeignKey(x => x.YearId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}