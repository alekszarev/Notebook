namespace Notebook.Data.Models.Calendar
{
    public class Day
    {
        public Day()
        {
            this.Notes = new HashSet<Note>();
            this.Hours = new HashSet<Hour>();
        }

        public int DayId { get; set; }

        public int Date { get; set; }
        public string DayOfWeek { get; set; }

        public bool? WorkingDay { get; set; }

        public int MonthId { get; set; }

        public Month Month { get; set; }
        public int Appointments { get; set; }
        public ICollection<Note> Notes { get; set; }
        public ICollection<Hour> Hours { get; set; }
    }
}
