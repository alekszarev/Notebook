using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Data.Models
{
    public class Message
    {
        public Message()
        {
            this.Date = DateTime.Now.ToString("dddd, dd MMMM yyyy");
        }
        public int MessageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public bool Seen { get; set; }
        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }


    }
}
