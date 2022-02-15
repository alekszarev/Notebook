using Notebook.Data.Models.Calendar;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Data.Models
{
    public class NotebookCalendar
    {
        public NotebookCalendar()
        {
            this.Years = new HashSet<Year>();
        }

        [Key]
        public int CalendarId { get; set; }

        public ICollection<Year> Years { get; set; }

    }
}
