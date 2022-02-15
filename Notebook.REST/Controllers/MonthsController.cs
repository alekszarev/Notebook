using Microsoft.AspNetCore.Mvc;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthsController : ControllerBase
    {
        private readonly IMonthService monthService;
        public MonthsController(IMonthService monthService)
        {
            this.monthService = monthService;
        }

        //GET api/months
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var months = this.monthService.GetAll();
            return this.Ok(months);
        }

        //GET api/months/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var month = this.monthService.Get(id);
            return this.Ok(month);
        }
    }
}
