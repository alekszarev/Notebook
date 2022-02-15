using Notebook.Data.Models.Calendar;

namespace Notebook.Service.Contracts
{
    public interface IAppointmentService
    {
        public void Delete(int id);
        public Appointment Get(int id);
        public Appointment SetStatus(int id);

        public IQueryable<Appointment> GetAll();

        public IQueryable<Appointment> ClientAppointments(string userId);


        public Appointment MakeAppointment(Appointment appointment);
    }
}
