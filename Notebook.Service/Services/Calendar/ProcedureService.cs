using Notebook.Data;
using Notebook.Data.Models;
using Notebook.Service.Contracts;
using Notebook.Service.Exceptions;

namespace Notebook.Service.Services.Calendar
{
    public class ProcedureService : IProcedureService
    {
        private readonly NotebookContext context;

        public ProcedureService(NotebookContext context)
        {
            this.context = context;
        }
        public Procedure Get(int id)
        {
            var procedure = this.context.Procedures.FirstOrDefault(x => x.ProcedureId == id);
            return procedure ?? throw new EntityNotFoundException();

        }

        public IEnumerable<Procedure> GetAll()
        {
            var procedures = this.context.Procedures;
            return procedures;
        }

        public Procedure Create(string name, int time)
        {
            var procedure = new Procedure()
            {
                ProcedureName = name,
                ProcedureTime = time
            };

            this.context.Procedures.Add(procedure);
            this.context.SaveChanges();

            return procedure;
        }
    }
}
