using Microsoft.EntityFrameworkCore.ChangeTracking;
using Notebook.Data.Models;

namespace Notebook.Service.Contracts
{
    public interface IUserBufferService
    {
        public UserBuffer Get(string username);

        public IEnumerable<UserBuffer> GetAll();

        public UserBuffer Create(string? name, string? phone, string? email, int? hourId, int? procedureId);
    }
}
