using Notebook.Data.Common;

namespace Notebook.Data.Models.Calendar
{
    public class Appointment
    {
        public int HourId { get; set; }

        public Hour Hour { get; set; }

        public bool? IsAproved { get; set; }

        public int? UserBufferId { get; set; }

        public UserBuffer? UserBuffer { get; set; }
        public string? UserId { get; set; }

        public User? User { get; set; }

        public int? ProcedureId { get; set; }

        public Procedure? Procedure { get; set; }

    }
}
