using Notebook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Service.Contracts
{
    public interface IMessageService
    {
        public void Delete(int id);
        public Message Get(int id);
        public IQueryable<Message> GetAll();
        public Message Send(Message message);
        public IQueryable<Message> ClientsGetall(string userId);


    }
}
