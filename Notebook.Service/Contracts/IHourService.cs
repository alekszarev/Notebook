using Notebook.Data.Models.Calendar;

namespace Notebook.Service.Contracts
{
    public interface IHourService
    {
        public void Delete(int id);
        public Hour Get(int id);

        public IQueryable<ICollection<Hour>> GetAll();

        public IQueryable<ICollection<Hour>> GetAvailableForDay(int dayId);

    }
}
