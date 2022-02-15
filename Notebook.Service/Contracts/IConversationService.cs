using Notebook.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Service.Contracts
{
    public interface IConversationService
    {
        Conversation Get(int id);

        IQueryable<Conversation> GetAll();

        Conversation Create(Conversation conversation);
    }
}
