using Microsoft.AspNetCore.Mvc;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarsController : ControllerBase
    {
        private readonly ICalendarService calendarService;
        public CalendarsController(ICalendarService calendarService)
        {
            this.calendarService = calendarService;
        }

        //GET api/calendars
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var calendars = this.calendarService.GetAll();
            return this.Ok(calendars);
        }

        //GET api/calendars/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var calendar = this.calendarService.Get(id);
            return this.Ok(calendar);
        }
    }
}
