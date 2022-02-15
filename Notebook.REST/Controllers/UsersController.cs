using Microsoft.AspNetCore.Mvc;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        //GET api/users
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var users = this.userService.GetAll();
            return this.Ok(users);
        }

        //GET api/users/:username
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            var user = this.userService.Get(username);
            return this.Ok(user);
        }
    }
}
