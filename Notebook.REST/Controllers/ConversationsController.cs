using Microsoft.AspNetCore.Mvc;
using Notebook.Data.Models;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationsController : ControllerBase
    {
        private readonly IConversationService conversationService;
        public ConversationsController(IConversationService conversationService)
        {
            this.conversationService = conversationService;
        }
        //GET api/conversations
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var conversations = this.conversationService.GetAll();
            return this.Ok(conversations);
        }

        //POST api/conversations
        [HttpPost("")]
        public IActionResult Create(Conversation conversation)
        {
            var conversationToCreate = this.conversationService.Create(conversation);
            return this.Ok(conversationToCreate);
        }
    }
}
