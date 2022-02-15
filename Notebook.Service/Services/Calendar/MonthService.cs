using Notebook.Data;
using Notebook.Data.Models.Calendar;
using Notebook.Service.Contracts;
using Notebook.Service.Exceptions;

namespace Notebook.Service.Services.Calendar
{
    public class MonthService : IMonthService
    {
        private readonly NotebookContext context;

        public MonthService(NotebookContext context)
        {
            this.context = context;
        }
        public Month Get(int id)
        {
            var month = this.context.Months.FirstOrDefault(x => x.MonthId == id);
            return month ?? throw new EntityNotFoundException();

        }

        public IQueryable<ICollection<Month>> GetAll()
        {
            var months = this.context.Calendar
            .SelectMany(x => x.Years)
            .Select(x => x.Months);

            return months;
        }
    }
}
