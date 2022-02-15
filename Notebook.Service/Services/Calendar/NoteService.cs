using Notebook.Data;
using Notebook.Data.Models.Calendar;
using Notebook.Service.Contracts;
using Notebook.Service.Exceptions;

namespace Notebook.Service.Services.Calendar
{
    public class NoteService : INoteService
    {
        private readonly NotebookContext context;

        public NoteService(NotebookContext context)
        {
            this.context = context;
        }
        public Note Get(int id)
        {
            var note = this.context.Notes.FirstOrDefault(x => x.NoteId == id);
            return note ?? throw new EntityNotFoundException();

        }

        public IQueryable<ICollection<Note>> GetAll()
        {
            return null;
        }
    }
}
