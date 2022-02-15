using Microsoft.EntityFrameworkCore;
using Notebook.Data;

namespace Notebook.Service.Services
{
    public class CalendarOldService : ICalendarOldService
    {
        public IDatabase context;

        public CalendarOldService(IDatabase context)
        {
            this.context = context;
        }

        public async Task<CalendarWeb> GetCalendarAsync(int yearId, int monthId, int? dayId, int? hourId, int? appointmentId)
        {
            if (monthId == 0)
            {
                monthId = DateTime.Now.Month;
            }

            var calendar = await this.context.Calendar
                .Include(x => x.Years)
                .ThenInclude(x => x.Months)
                .ThenInclude(x => x.Days)
                .ThenInclude(x => x.Hours)
                .FirstOrDefaultAsync(x => x.CalendarId == 1);


            var firstDay = new DateTime(DateTime.UtcNow.Year, monthId, 1);
            string day = firstDay.DayOfWeek.ToString();

            int nullTables = SwitchDay(day) - 1;

            var calendarView = new CalendarWeb();
            calendarView.Year = calendar.Years.FirstOrDefault(x => x.YearId == yearId);
            calendarView.Month = calendarView.Year.Months.FirstOrDefault(x => x.MonthId == monthId);
            calendarView.nullTables = nullTables;


            return calendarView;
        }

        static int SwitchDay(string day)
        {
            switch (day.ToLower())
            {
                case "monday":
                    return 1;

                case "tuesday":
                    return 2;

                case "wednesday":
                    return 3;

                case "thursday":
                    return 4;

                case "friday":
                    return 5;

                case "saturday":
                    return 6;

                case "sunday":
                    return 7;

                default:
                    return 0;
            }
        }
    }


}
