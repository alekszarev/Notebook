using System.ComponentModel.DataAnnotations;

namespace Notebook.Data.Models.Calendar
{
    public class Year
    {
        public Year()
        {
            this.Months = new HashSet<Month>();
        }
        [Key]
        public int YearId { get; set; }

        public int YearNum { get; set; }

        public int CalendarId { get; set; }
        public NotebookCalendar Calendar { get; set; }

        public ICollection<Month> Months { get; set; }
    }
}
