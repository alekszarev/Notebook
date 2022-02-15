
using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Data.Models.Calendar;
using Notebook.Service.Contracts;
using Notebook.Service.Exceptions;

namespace Notebook.Service.Services.Calendar
{
    public class HourService : IHourService
    {
        private readonly NotebookContext context;

        public HourService(NotebookContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var hour = Get(id);
            this.context.Hours.Remove(hour);
            this.context.SaveChanges();
        }
        public Hour Get(int id)
        {
            var hour = this.context.Hours
                .Include(x=>x.Day)
                .ThenInclude(x=>x.Month)
                .Include(x => x.Appointment)
                .FirstOrDefault(x => x.HourId == id);
            return hour ?? throw new EntityNotFoundException();

        }

        public IQueryable<ICollection<Hour>> GetAll()
        {
            var hours = this.context.Calendar
          .SelectMany(x => x.Years)
          .SelectMany(x => x.Months)
          .SelectMany(x => x.Days)
          .Select(x => x.Hours);
          
            return hours;

        }
        public IQueryable<ICollection<Hour>> GetAvailableForDay(int dayId)
        {
            var hours = GetAll().Where(x => x.Any(x => x.DayId == dayId));
            return hours;

        }
    }
}
