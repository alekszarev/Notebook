using Notebook.Data;
using Notebook.Data.Models.Calendar;
using Notebook.Service.Contracts;
using Notebook.Service.Exceptions;

namespace Notebook.Service.Services.Calendar
{
    public class YearService : IYearService
    {
        private readonly NotebookContext context;

        public YearService(NotebookContext context)
        {
            this.context = context;
        }
        public Year Get(int id)
        {
            var year = this.context.Years.FirstOrDefault(x => x.YearId == id);
            return year ?? throw new EntityNotFoundException();

        }

        public IQueryable<ICollection<Year>> GetAll()
        {
            var years = this.context.Calendar
                .Select(x => x.Years);
            return years;
        }
    }
}
