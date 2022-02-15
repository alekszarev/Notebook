using Notebook.Data.Models.Calendar;

namespace Notebook.Service.Contracts
{
    public interface IMonthService
    {
        public Month Get(int id);

        public IQueryable<ICollection<Month>> GetAll();

    }
}
