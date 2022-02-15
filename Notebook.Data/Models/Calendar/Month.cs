namespace Notebook.Data.Models.Calendar
{
    public class Month
    {
        public Month()
        {
            this.Days = new HashSet<Day>();
        }
        public int MonthId { get; set; }
        public int MonthNum { get; set; }

        public string MonthName { get; set; }
        public int YearId { get; set; }
        public Year Year { get; set; }
        public ICollection<Day> Days { get; set; }
    }
}
