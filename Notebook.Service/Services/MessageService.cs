using Microsoft.EntityFrameworkCore;
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
    public class MessageService : IMessageService
    {
        private readonly NotebookContext context;

        public MessageService(NotebookContext context)
        {
            this.context = context;
        }
        public IQueryable<Message> ClientsGetall(string userId)
        {
           var messages = GetAll();
            return messages;
        }

        public void Delete(int id)
        {
            var message = Get(id);
            this.context.Messages.Remove(message);
            this.context.SaveChanges();
        }

        public Message Get(int id)
        {
            var message = this.context.Messages.FirstOrDefault(x=>x.MessageId == id);
            return message;
        }

        public IQueryable<Message> GetAll()
        {
            var messages = this.context.Messages
                .Include(x => x.Conversation);

            return messages;
        }

        public Message Send(Message messageToSend)
        {
            
            this.context.Messages.Add(messageToSend);
            this.context.SaveChanges();

            var message = Get(messageToSend.MessageId);
            return message;
        }
    }
}
