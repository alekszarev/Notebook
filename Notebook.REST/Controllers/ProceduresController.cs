using Microsoft.AspNetCore.Mvc;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProceduresController : ControllerBase
    {
        private readonly IProcedureService procedureService;
        public ProceduresController(IProcedureService procedureService)
        {
            this.procedureService = procedureService;
        }

        //GET api/procedures
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var procedures = this.procedureService.GetAll();
            return this.Ok(procedures);
        }

        //GET api/hours/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var procedure = this.procedureService.Get(id);
            return this.Ok(procedure);
        }

        //POST api/hours
        [HttpPost("")]
        public IActionResult Create(string name, int id)
        {
            var procedure = this.procedureService.Create(name, id);
            return this.Ok(procedure);
        }
    }
}
