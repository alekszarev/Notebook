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
    public class UserBufferService : IUserBufferService
    {
        private readonly NotebookContext context;
        public UserBufferService(NotebookContext context)
        {
            this.context = context;
        }

        public UserBuffer Create(string? name, string? phone, string? email, int? hourId, int? procedureId)
        {
            var user = new UserBuffer();
            if(name != null)
            {
                user.Name = name;
            }
            if(phone != null)
            {
                user.Phone = phone;
            }
            if(email != null)
            {
                user.Email = email;
            }
            if(hourId != null)
            {
                user.HourId = hourId;
            }
            if(procedureId != null)
            {
                user.ProcedureId = procedureId;
            }

            this.context.UserBuffers.Add(user);
            this.context.SaveChanges();

            return user;
        }

        public UserBuffer Get(string fullName)
        {
            var user = this.context.UserBuffers.FirstOrDefault(x => x.Name == fullName);
            return user;
        }

        public IEnumerable<UserBuffer> GetAll()
        {
            var users = this.context.UserBuffers
                .Include(x => x.Appointments);

            return users;
        }


    }
}
