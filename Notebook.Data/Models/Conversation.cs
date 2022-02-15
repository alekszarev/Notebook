using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Data.Models
{
    public class Conversation
    {
        public Conversation()
        {
            this.Messages = new HashSet<Message>();
            this.Users = new HashSet<User>();
        }
        public int ConversationId { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
