using Microsoft.AspNetCore.Identity;

namespace Notebook.Data.Models
{
    public class Role : IdentityRole
    {
        public Role()
            : this(null)
        {
        }

        public Role(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }
}
