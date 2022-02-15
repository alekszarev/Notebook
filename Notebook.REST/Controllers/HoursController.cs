using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HoursController : ControllerBase
    {
        private readonly IHourService hourService;
        public HoursController(IHourService hourService)
        {
            this.hourService = hourService;
        }

        //GET api/hours
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var hours = await this.hourService.GetAll().ToListAsync();
            return this.Ok(hours);

        }

        //GET api/hours/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var hour = this.hourService.Get(id);
            return this.Ok(hour);
        }
    }
}
