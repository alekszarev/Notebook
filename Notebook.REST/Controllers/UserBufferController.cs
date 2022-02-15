using Microsoft.AspNetCore.Mvc;
using Notebook.Data.Models;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserBufferController : ControllerBase
    {
        private readonly IUserBufferService userBufferService;
        public UserBufferController(IUserBufferService userBufferService)
        {
            this.userBufferService = userBufferService;
        }

        //GET api/userBuffers
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var users = this.userBufferService.GetAll();
            return this.Ok(users);
        }

        //GET api/userBuffers/:username
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var user = this.userBufferService.Get(username);
            return this.Ok(user);
        }

        //POST api/userBuffers/
        [HttpPost("")]
        public IActionResult Create([FromForm] string? name, [FromForm] string? phone, [FromForm] string? email, [FromForm] int? hourId, [FromForm] int? procedureId)
        {
            var user = this.userBufferService.Create(name,phone,email, hourId,procedureId);
            return this.Ok(user);
        }
    }
}
