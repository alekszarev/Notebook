using Notebook.Data.Common;

namespace Notebook.Data.Models.Calendar
{
    public class Hour 
    {
        public Hour()
        {
        }

        public int HourId { get; set; }
        public int Clock { get; set; }
        public int DayId { get; set; }
        public Day Day { get; set; }

        public int AppointmentId { get; set; }

        public Appointment Appointment { get; set; }
        public bool IsBusy { get; set; }
    }
}