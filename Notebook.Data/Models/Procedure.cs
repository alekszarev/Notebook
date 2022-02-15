using Notebook.Data.Common;
using Notebook.Data.Models.Calendar;
using System.ComponentModel.DataAnnotations;

namespace Notebook.Data.Models
{
    public class Procedure : NotebookInformation
    {
        public Procedure()
        {
            this.Appointments = new HashSet<Appointment>();
        }

        [Key]
        public int ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public int ProcedureTime { get; set; }

        public ICollection<Appointment> Appointments { get; set; }


    }
}
