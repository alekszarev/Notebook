using Notebook.Data.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Data.Models
{
    public class UserBuffer
    {
        public UserBuffer()
        {
            this.CreatedOn = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }

        public int UserBuferId { get; set; }
        public bool GotAppointment { get; set; } = false;

        public int? HourId { get; set; }
        public int? ProcedureId { get; set; }
        public string? Name { get; set; }

        public string? Email {get; set;}
        public string? Phone { get; set; }
        public string CreatedOn { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
