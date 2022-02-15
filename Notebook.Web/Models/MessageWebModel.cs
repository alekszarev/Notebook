using Microsoft.AspNetCore.Mvc.Rendering;

namespace Notebook.Web.Models
{
    public class MessageWebModel
    {      

        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string UserSenderId { get; set; }
        public string UserRecieverId { get; set; }

        public SelectList ListUsers { get; set; }
    }
}
