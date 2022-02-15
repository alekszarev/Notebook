using Notebook.Data.Models;

namespace Notebook.Service.Contracts
{
    public interface IProcedureService
    {
        public Procedure Get(int id);

        public IEnumerable<Procedure> GetAll();

        public Procedure Create(string name, int time);
    }
}
