using Notebook.Data;
using Notebook.Data.Models;
using Notebook.Service.Contracts;

namespace Notebook.Service.Services
{
    public class UserService : IUserService
    {
        private readonly NotebookContext context;

        public UserService(NotebookContext context)
        {
            this.context = context;
        }
        public User Get(string username)
        {
            var user = this.context.Users.FirstOrDefault(x => x.UserName == username);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            var users = this.context.Users;
            return users;
        }
    }
}
