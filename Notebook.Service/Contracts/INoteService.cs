using Notebook.Data.Models.Calendar;

namespace Notebook.Service.Contracts
{
    public interface INoteService
    {
        public Note Get(int id);

        public IQueryable<ICollection<Note>> GetAll();

    }
}
