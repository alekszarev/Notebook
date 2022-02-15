using Microsoft.EntityFrameworkCore.Query;
using Notebook.Data.Models.Calendar;

namespace Notebook.Service.Contracts
{
    public interface IDayService
    {
        public Day Get(int id);

        public IIncludableQueryable<Day, IEnumerable<Hour>> GetAll();

        public Day SetWorkingDay(int dayId, bool isWorking);

    }
}
