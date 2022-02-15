using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notebook.Data.Models;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messageService;
        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        //GET api/messages
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var messages = this.messageService.GetAll();

            return this.Ok(messages);
        }

        //GET api/messages/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var messages = this.messageService.Get(id);
            return this.Ok(messages);
        }

        //POST api/messages/
        [HttpPost("")]
        public IActionResult Send(Message messageToSend)
        {   

            var message = this.messageService.Send(messageToSend);
            return this.Ok(message);
        }
    }
}
