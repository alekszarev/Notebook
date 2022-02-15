using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Data.Models.Calendar;
using Notebook.Service.Contracts;
using System.Security.Claims;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;
        public AppointmentsController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        //POST api/appointments
        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult MakeAppointment(Appointment app)
        {
            var appointment = this.appointmentService.MakeAppointment(app);
            return this.Ok(appointment);
        }

        //GET api/appointments
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var appointments = this.appointmentService.GetAll()
                .Include(x => x.Hour)
                .ThenInclude(x => x.Day)
                .ThenInclude(x => x.Month);

            return this.Ok(appointments);
        }

        //GET api/appointments/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var appointment = this.appointmentService.Get(id);
            return this.Ok(appointment);
        }


        //GET api/appointments/waiting
        [HttpGet("waiting")]
        public IActionResult ForApprovement()
        {
            var appointments = this.appointmentService.GetAll()
                .Where(x=>x.IsAproved==false)
                .Include(x => x.Hour)
                .ThenInclude(x => x.Day)
                .ThenInclude(x => x.Month);

            return this.Ok(appointments);
        }




    }
}
