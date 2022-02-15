using Notebook.Data.Models;

namespace Notebook.Service.Contracts
{
    public interface IUserService
    {
        public User Get(string username);

        public IEnumerable<User> GetAll();
    }
}
