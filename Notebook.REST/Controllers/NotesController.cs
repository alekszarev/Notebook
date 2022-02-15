using Microsoft.AspNetCore.Mvc;
using Notebook.Service.Contracts;

namespace Notebook.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService noteService;
        public NotesController(INoteService noteService)
        {
            this.noteService = noteService;
        }

        //GET api/notes
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var notes = this.noteService.GetAll();
            return this.Ok(notes);
        }

        //GET api/notes/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var note = this.noteService.Get(id);
            return this.Ok(note);
        }
    }
}
