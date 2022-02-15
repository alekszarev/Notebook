using Notebook.Data.Models;

namespace Notebook.Service.Contracts
{
    public interface ICalendarService
    {
        public NotebookCalendar Get(int id);

        public IEnumerable<NotebookCalendar> GetAll();
    }
}
