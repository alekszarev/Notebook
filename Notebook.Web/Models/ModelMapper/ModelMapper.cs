using Notebook.Data.Models;
using Notebook.Data.Models.Calendar;

namespace Notebook.Web.Models.ModelMapper
{
    public class ModelMapper
    {
        public UserBuffer ToModel(UserBufferWebModel userWeb)
        {
            var userBuffer = new UserBuffer
            {
                Email = userWeb.Email,
                Phone = userWeb.Phone,
                HourId = userWeb.HourId,
                Name = userWeb.Name,
                ProcedureId = userWeb.ProcedureId,
            };

            return userBuffer;
        }
        public Message ToModel(MessageWebModel messageWeb)
        {
            var message = new Message
            {
                Title = messageWeb.Title,
                Content = messageWeb.Content
            };

           return message;
        }
        public  Appointment ToModel(SendModel send)
        {
            var appointment = new Appointment
            {
                HourId = send.Hour.HourId,
                Hour = send.Hour,
                ProcedureId = (int)send.ProcedureId,
                UserId = send.UserId,
                
            };

            return appointment;

        }

        public AppointmentWebModel ToModel(Appointment appointment)
        {

            var model = new AppointmentWebModel();
            if (appointment.User.Name == null)
            {
                model.UserName = appointment.User.Email;
            }
            else
            {
                model.UserName = appointment.User.Name;
            }

            model.Id = appointment.HourId;
            model.Clock = appointment.Hour.Clock;
            model.Day = $"{appointment.Hour.Day.Date} {appointment.Hour.Day.Month.MonthName}";
            model.WeekDay = appointment.Hour.Day.DayOfWeek;

            return model;
        }


    }
}
