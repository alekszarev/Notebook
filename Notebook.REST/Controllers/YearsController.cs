using Microsoft.AspNetCore.Mvc;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YearsController : ControllerBase
    {
        private readonly IYearService yearService;
        public YearsController(IYearService yearService)
        {
            this.yearService = yearService;
        }

        //GET api/years
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var years = this.yearService.GetAll();
            return this.Ok(years);
        }

        //GET api/hours/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var year = this.yearService.Get(id);
            return this.Ok(year);
        }
    }
}
