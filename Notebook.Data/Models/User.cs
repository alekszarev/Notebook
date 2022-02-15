using Microsoft.AspNetCore.Identity;
using Notebook.Data.Common;
using Notebook.Data.Models.Calendar;

namespace Notebook.Data.Models
{
    public class User : IdentityUser, INotebookInformation
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Appointments = new HashSet<Appointment>();
            this.CreatedOn = DateTime.Now.ToString("dddd, dd MMMM yyyy");
            this.Conversations = new HashSet<Conversation>();

        }

        public bool GotAppointment { get; set; } = false;
        public string? Name { get; set; }
        public string CreatedOn { get; set; }
        public string? ModifiedOn { get; set; }
        public bool IsDelete { get; set; }
        public string? DeletedOn { get; set; }

        public virtual ICollection<Conversation> Conversations { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
