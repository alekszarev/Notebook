using Notebook.Data.Models.Calendar;

namespace Notebook.Service
{
    public class DayWeb
    {
        public Month Month { get; set; }
        public Day Day { get; set; }
        public ICollection<Note>? Notes { get; set; }
        public ICollection<Hour> Hours { get; set; }


    }
}

