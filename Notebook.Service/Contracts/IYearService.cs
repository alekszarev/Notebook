using Notebook.Data.Models.Calendar;

namespace Notebook.Service.Contracts
{
    public interface IYearService
    {
        public Year Get(int id);

        public IQueryable<ICollection<Year>> GetAll();

    }
}
