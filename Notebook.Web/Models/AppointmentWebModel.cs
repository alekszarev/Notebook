using Notebook.Data.Models;

namespace Notebook.Web.Models
{
    public class AppointmentWebModel
    {
        public int Id { get; set; } 
        public string CreatedOn { get; set; }

        public int Clock { get; set; }

        public string Day { get; set; }

        public int? DayId { get; set; }

        public string UserName { get; set; }

        public string WeekDay { get; set; }
    }
}
