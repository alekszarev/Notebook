using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Data.Models.Calendar;

namespace Notebook.Data.ModelsConfig
{
    public class NoteConfig : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(x => x.NoteId);

            builder.HasOne(x => x.Day)
                .WithMany(x => x.Notes)
                              .HasForeignKey(x => x.DayId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}