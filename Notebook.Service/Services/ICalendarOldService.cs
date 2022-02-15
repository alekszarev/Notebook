namespace Notebook.Service.Services
{
    public interface ICalendarOldService
    {
        public Task<CalendarWeb> GetCalendarAsync(int yearId, int monthId, int? dayId, int? hourId, int? appointmentId);

    }
}
