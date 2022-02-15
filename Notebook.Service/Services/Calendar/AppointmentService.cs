using Microsoft.EntityFrameworkCore;
using Notebook.Data;
using Notebook.Data.Models.Calendar;
using Notebook.Service.Contracts;
using Notebook.Service.Exceptions;
using System.Web.Http;

namespace Notebook.Service.Services.Calendar
{
    public class AppointmentService : IAppointmentService
    {
        private readonly NotebookContext context;
        private readonly IHourService hourService;
        private readonly IUserService userService;

        public AppointmentService(NotebookContext context, IHourService hourService, IUserService userService)
        {
            this.context = context;
            this.userService = userService;
            this.hourService = hourService;
        }

        public void Delete(int id)
        {

            var app = Get(id);
            if (app.ProcedureId >= 2)
            {
                var hour = this.context.Hours
                    .FirstOrDefault(x => x.HourId == (app.HourId));
                var hour2 = this.context
                    .Hours
                    .FirstOrDefault(x => x.HourId == (app.HourId + 1));

                hour.IsBusy = false;
                hour2.IsBusy = false;
            }
            else
            {
                var hour = this.context
                    .Hours
                    .FirstOrDefault(x => x.HourId == (app.HourId));

                hour.IsBusy = false;
            }
            var user = this.context.Users
            .Include(x => x.Appointments)
            .ThenInclude(x => x.Hour).ThenInclude(x => x.Day).ThenInclude(x => x.Month)
            .FirstOrDefault(x => x.Id == app.UserId);

            var day = this.context.Days.FirstOrDefault(x => x.DayId == this.context.Hours.FirstOrDefault(x => x.HourId == app.HourId).DayId);
            day.Appointments--;

            user.GotAppointment = false;
            this.context.Appointments.Remove(app);
            this.context.SaveChanges();

        }
        public Appointment MakeAppointment(Appointment app)
        {
            var user = this.context.Users
                .Include(x => x.Appointments)
                .ThenInclude(x => x.Hour).ThenInclude(x => x.Day).ThenInclude(x => x.Month)
                .FirstOrDefault(x => x.Id == app.UserId);

            var today = this.context.Days
                .Include(x => x.Hours)
                .FirstOrDefault(x => x.Date == DateTime.Now.Day && x.Month.MonthNum == DateTime.Now.Month);
            if (user != null)
            {
                if (user.Appointments.Count != 0)
                {
                    var list = user.Appointments.ToList()
                        .OrderByDescending(x => x.HourId);

                    if (list != null && (today.Hours.FirstOrDefault()
                        .HourId < list.FirstOrDefault()
                        .HourId))
                    {
                        throw new ApplicationException($"Имате записан час на {user.Appointments.FirstOrDefault().Hour.Day.Date} {user.Appointments.FirstOrDefault().Hour.Day.Month.MonthName} {user.Appointments.FirstOrDefault().Hour.Clock}:00");
                    }
                    if (user.GotAppointment == true && (today.Hours.FirstOrDefault().HourId == list.FirstOrDefault().HourId))
                    {
                        Console.WriteLine("OK");
                    }

                }
            }

            var appointment = new Appointment();
            if (app.ProcedureId >= 2)
            {
                {
                    appointment.HourId = app.HourId;
                    appointment.Hour = this.context.Hours.FirstOrDefault(x => x.HourId == app.HourId);
                    if (user != null)
                    {
                        appointment.UserId = user.Id;
                        appointment.IsAproved = false;
                    }
                    else
                    {
                        appointment.UserBufferId = app.UserBufferId;
                        appointment.IsAproved = true;
                    }
                    appointment.ProcedureId = app.ProcedureId;
                    appointment.Procedure = this.context.Procedures.FirstOrDefault(x => x.ProcedureId == app.ProcedureId);

                };

                var hour = this.context.Hours.FirstOrDefault(x => x.HourId == (appointment.HourId));
                var hour2 = this.context.Hours.FirstOrDefault(x => x.HourId == (appointment.HourId + 1));
                hour.IsBusy = true;
                hour2.IsBusy = true;

                if (user != null)
                {
                    user.GotAppointment = true;
                }

                this.context.SaveChanges();
            }

            else if (app.ProcedureId < 2)
            {
                appointment.HourId = app.HourId;
                appointment.Hour = this.context.Hours.FirstOrDefault(x => x.HourId == app.HourId);
                if (user != null)
                {
                    appointment.UserId = user.Id;
                    appointment.IsAproved = false;
                }
                else
                {
                    appointment.UserBufferId = app.UserBufferId;
                    appointment.IsAproved = true;
                }
                appointment.ProcedureId = app.ProcedureId;
                appointment.Procedure = this.context.Procedures.FirstOrDefault(x => x.ProcedureId == app.ProcedureId);

                if (user != null)
                {
                    user.GotAppointment = true;
                }
                var hour = this.context.Hours.FirstOrDefault(x => x.HourId == (appointment.HourId));
                hour.IsBusy = true;
                this.context.SaveChanges();

            }

            var dayId = this.context.Hours.FirstOrDefault(x => x.HourId == app.HourId).DayId;
            var day = this.context.Days.FirstOrDefault(x => x.DayId == dayId);
            day.Appointments++;


            this.context.Appointments.Add(appointment);
            this.context.SaveChanges();
            return appointment;
        }

        public Appointment Get(int id)
        {
            var appointment = GetAll()
                .Include(x => x.Hour)
                .ThenInclude(x => x.Day)
                .ThenInclude(x => x.Month)
                .Include(x => x.User)
                .Include(x => x.UserBuffer)
                .Include(x => x.Procedure)
                .FirstOrDefault(x => x.HourId == id);
            return appointment ?? throw new EntityNotFoundException();

        }
        [Authorize(Roles = "Owner")]
        public IQueryable<Appointment> GetAll()
        {
            var appointments = this.context.Appointments;

            return appointments;
        }
        [Authorize(Roles = "Client")]
        public IQueryable<Appointment> ClientAppointments(string userId)
        {
            var forAproving = GetAll()
                .Where(x => x.UserId == userId);

            return forAproving;
        }

        public Appointment SetStatus(int id)
        {
            var app = Get(id);
            if (app.IsAproved == false)
            {
                app.IsAproved = true;
            }
            else if (app.IsAproved == true)
            {
                app.IsAproved = false;
            }
            this.context.SaveChanges();
            return app;

        }
    }
}
