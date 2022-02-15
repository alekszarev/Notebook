using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Data.Models;
using Notebook.Service.Contracts;
using Notebook.Service.Exceptions;

namespace Notebook.Service.Services.Calendar
{
    public class CalendarService : ICalendarService
    {
        private readonly NotebookContext context;

        public CalendarService(NotebookContext context)
        {
            this.context = context;
        }

        public NotebookCalendar Get(int id)
        {
            var calendar = this.context.Calendar.FirstOrDefault(x => x.CalendarId == id);
            return calendar ?? throw new EntityNotFoundException();
        }

        public IEnumerable<NotebookCalendar> GetAll()
        {
            var calendars = this.context.Calendar
                .Include(x => x.Years);
            return calendars;
        }
    }
}
