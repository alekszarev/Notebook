using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DaysController : ControllerBase
    {
        private readonly IDayService dayService;
        public DaysController(IDayService dayService)
        {
            this.dayService = dayService;
        }

        //GET api/days
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var days = this.dayService.GetAll();
            return this.Ok(days);
        }

        //GET api/days/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var day = this.dayService.Get(id);
            return this.Ok(day);
        }

        //GET api/days/:id/hours
        [HttpGet("{id}/hours")]
        public IActionResult GetHours(int id)
        {
            var day = this.dayService.GetAll()
                .Include(x => x.Hours).ThenInclude(x => x.Appointment)
                .FirstOrDefault(x => x.DayId == id);
            return this.Ok(day);
        }

        //PUT api/days/:id
        [HttpPut("{id}")]
        public IActionResult SetWorkingDay(int id, bool isWorking)
        {
            var day = this.dayService.SetWorkingDay(id, isWorking);
            return this.Ok(day);
        }
    }
}
