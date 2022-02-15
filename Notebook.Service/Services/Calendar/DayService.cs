using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Notebook.Data;
using Notebook.Data.Models.Calendar;
using Notebook.Service.Contracts;
using Notebook.Service.Exceptions;

namespace Notebook.Service.Services.Calendar
{
    public class DayService : IDayService
    {
        private readonly NotebookContext context;

        public DayService(NotebookContext context)
        {
            this.context = context;
        }
        public Day Get(int id)
        {
            var day = GetAll()
                .Include(x=>x.Hours)
                .ThenInclude(x=>x.Appointment)
                .ThenInclude(x=>x.Procedure)
                .Include(x => x.Hours)
                .ThenInclude(x => x.Appointment).ThenInclude(x=>x.User)
                .Include(x => x.Hours)
                .ThenInclude(x => x.Appointment).ThenInclude(x => x.UserBuffer)
                .Include(x => x.Month)
                .FirstOrDefault(x => x.DayId == id);


            return day ?? throw new EntityNotFoundException();

        }

        public IIncludableQueryable<Day, IEnumerable<Hour>> GetAll()
        {
            var days = this.context.Calendar
           .SelectMany(x => x.Years)
           .SelectMany(x => x.Months)
           .SelectMany(x => x.Days)
           .Include(x => x.Hours);

            return days;

        }

        public Day SetWorkingDay(int dayId, bool isWorking)
        {

            var day = GetAll().FirstOrDefault(x => x.DayId == dayId);
            day.WorkingDay = isWorking;
            this.context.SaveChangesAsync();

            return day;

        }
    }
}
