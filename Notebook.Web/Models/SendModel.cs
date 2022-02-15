using Microsoft.AspNetCore.Mvc.Rendering;
using Notebook.Data.Models;
using Notebook.Data.Models.Calendar;

namespace Notebook.Web.Models
{
    public class SendModel
    {
        public int DayId { get; set; }
        public Day Day { get; set; }
        public int HourId { get; set; }
        public Hour Hour { get; set; }

        public int? ProcedureId { get; set; }
        public SelectList Procedure { get; set; }

        public string UserId { get; set; }

    }
}
