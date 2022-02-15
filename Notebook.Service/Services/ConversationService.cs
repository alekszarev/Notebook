using Notebook.Data;
using Notebook.Data.Models;
using Notebook.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Service.Services
{
    public class ConversationService : IConversationService
    {
        private readonly NotebookContext context;
        public ConversationService(NotebookContext context)
        {
            this.context = context;
        }
        public Conversation Create(Conversation conversation)
        {
            var conversationToCreate = this.context.Conversations.Add(conversation);
            this.context.SaveChanges();
            return conversation;

        }

        public Conversation Get(int id)
        {
            var conversation = this.context.Conversations.FirstOrDefault(x=>x.ConversationId == id);
            return conversation;
        }

        public IQueryable<Conversation> GetAll()
        {
            var conversation = this.context.Conversations;
            return conversation;
        }
    }
}
