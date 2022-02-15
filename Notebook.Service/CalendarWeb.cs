using Notebook.Data.Models.Calendar;

namespace Notebook.Service
{

    public class CalendarWeb
    {
        public Year Year { get; set; }
        public Month Month { get; set; }

        public int nullTables { get; set; }
        public Day Day { get; set; }
        public Hour Hour { get; set; }
        public Appointment Appointment { get; set; }

    }
}
