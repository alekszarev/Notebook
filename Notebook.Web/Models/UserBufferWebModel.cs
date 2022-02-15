using Microsoft.AspNetCore.Mvc.Rendering;

namespace Notebook.Web.Models
{
    public class UserBufferWebModel
    {

        public int? HourId { get; set; }
        public int? ProcedureId { get; set; }
        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }

        public SelectList Procedure { get; set; }

        public string? Date { get; set; }
    }
}
