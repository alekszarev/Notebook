using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notebook.Data.Models;

namespace Notebook.Data.ModelsConfig
{
    public class NotebookCalendarConfig : IEntityTypeConfiguration<NotebookCalendar>
    {
        public void Configure(EntityTypeBuilder<NotebookCalendar> builder)
        {
            builder.HasKey(x => x.CalendarId);
        }
    }
}

