using Microsoft.AspNetCore.Identity;
using Notebook.Data.Common;

namespace Notebook.Data.Models
{
    public class Role : IdentityRole, INotebookElement
    {
        public Role()
            : this(null)
        {
        }

        public Role(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? DeletedOn { get; set; }

    }
}
